using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Shared.Exceptions;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Shared.Request;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Payments.Specifications;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Domain.Tenants.Specifications;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers;

public class PaymentReceivedConsumer(IUnitOfWork unitOfWork, ILogger<PaymentReceivedConsumer> logger, IEmailService emailService, IAsaasService asaasService,
    IPasswordHash passwordHash, IUserRuleCheck userRuleCheck) : IConsumer<PaymentReceivedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PaymentReceivedIntegrationEvent> context)
    {
        var msg = context.Message;
        logger.LogInformation("Processing PaymentReceivedIntegrationEvent for PaymentAsaasId={PaymentAsaasId}, SubscriptionAsaasId={SubscriptionAsaasId}",
            msg.PaymentAsaasId, msg.SubscriptionAsaasId);

        var payment = await unitOfWork.Payments.SingleOrDefaultAsync(new GetPaymentByAsaasIdSpecification(msg.PaymentAsaasId));
        if (payment is null){
            logger.LogWarning("Payment not found. PaymentAsaasId={PaymentAsaasId}", msg.PaymentAsaasId);
            return;
        }

        if (payment.Status == PaymentStatus.RECEIVED)
        {
            logger.LogWarning("Payment already received. PaymentAsaasId={PaymentAsaasId}", msg.PaymentAsaasId);
            return;
        }

        var isUpgrade = msg.ExternalReference?.StartsWith("upgrade:") == true;
        var subscriptionId = msg.ExternalReference != null ? msg.ExternalReference!.Replace("upgrade:", "") : msg.SubscriptionAsaasId;

        var subscription = await unitOfWork.Subscriptions.SingleOrDefaultAsync(new GetSubscriptionByAsaasIdSpecification(subscriptionId!));

        if (subscription is null) throw new IntegrationException($"subscription asaas with id:{msg.SubscriptionAsaasId} not found.");

        await unitOfWork.BeginTransaction();

        payment.ConfirmSubscriptionPayment(msg.ConfirmedDate!.Value);

        logger.LogInformation("Payment confirmed. PaymentAsaasId={PaymentAsaasId}", msg.PaymentAsaasId);

        if (isUpgrade)
        {
            if (subscription.PendingUpgradePlanId == null)
            {
                logger.LogWarning("No pending upgrade to apply for SubscriptionId={SubscriptionId}", subscription.Id);
                return;
            }

            var oldPlanName = subscription.SubscriptionPlan.Name;
            var newPlan = subscription.PendingUpgradePlan;
            var updateRequest = new UpdateSubscriptionRequest(
                payment.BillingType,
                subscription.PendingUpgradePlan!.Price,
                subscription.PendingUpgradePlan.Description
            );
            await asaasService.UpdateSubscription(subscriptionId, updateRequest);

            subscription.ChangePlan(subscription.PendingUpgradePlanId!);
            subscription.ConfirmUpgrade();
            subscription.Reactivate();

            await unitOfWork.CompleteAsync();
            await unitOfWork.CommitAsync();

            logger.LogInformation(
                "Upgrade confirmed. Tenant={TenantName}, OldPlan={OldPlan}, NewPlan={NewPlan}, SubscriptionId={SubscriptionId}",
                subscription.Tenant.Name,
                oldPlanName,
                subscription.SubscriptionPlan.Name,
                subscription.Id
            );

            await emailService.SendConfirmUpgradePlan(
                subscription.Tenant.Contact.Email, subscription.Tenant.Name, newPlan!.Name, newPlan.Price
            );

            return;
        }

        if (subscription.Status == SubscriptionStatusEnum.Pending)
        {
            subscription.Activate();
            logger.LogInformation("Subscription activated. SubscriptionId={SubscriptionId}", subscription.Id);

            var tenant = await unitOfWork.Tenants.SingleOrDefaultAsync(new GetTenantByIdGlobalSpecification(subscription.TenantId));
            var hasUsers = await unitOfWork.Users.AnyAsync(new GetUsersByTenantIdGlobalSpecification(tenant!.Id));

            if (!hasUsers)
            {
                var adminUser = User.Create(
                    tenant.Name,
                    tenant.Contact,
                    UserRoleEnum.AdminTenant,
                    tenant.Id,
                    passwordHash,
                    userRuleCheck
                );

                await unitOfWork.Users.AddAsync(adminUser);
                logger.LogInformation("Admin user created for TenantId={TenantId}", tenant.Id);
            }
        }
        else if (subscription.Status is SubscriptionStatusEnum.Expired
        or SubscriptionStatusEnum.Canceled
        or SubscriptionStatusEnum.Suspended or SubscriptionStatusEnum.Overdue)
        {
            subscription.Reactivate();
            logger.LogInformation("Subscription reactivated. SubscriptionId={SubscriptionId}", subscription.Id);
        }
        else
        {
            logger.LogInformation("Payment received for active subscription. SubscriptionId={SubscriptionId}", subscription.Id);
        }

        subscription.ResetProposalsUsed();

        await unitOfWork.CompleteAsync();
        await unitOfWork.CommitAsync();

        try
        {
            await emailService.SendConfirmPayment(
                subscription.Tenant.Contact.Email, subscription.Tenant.Name, msg.Amount, msg.PaymentDate, subscription.SubscriptionPlan.Name
            );

            logger.LogInformation("Confirmation email sent to {Email}", subscription.Tenant.Contact.Email);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send confirmation email to {Email}", subscription.Tenant.Contact.Email);
        }
    }
}

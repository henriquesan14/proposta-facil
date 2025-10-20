using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Payments.Specifications;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Domain.Tenants.Specifications;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers;

public class PaymentCreatedConsumer(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    ILogger<PaymentCreatedConsumer> logger
) : IConsumer<PaymentCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PaymentCreatedIntegrationEvent> context)
    {
        var msg = context.Message;

        logger.LogInformation("Received PaymentCreatedIntegrationEvent for SubscriptionAsaasId={SubscriptionAsaasId}, PaymentAsaasId={PaymentAsaasId}, Value={PaymentValue}",
            msg.SubscriptionAsaasId, msg.PaymentAsaasId, msg.PaymentValue);

        var isUpgrade = msg.ExternalReference?.StartsWith("upgrade:") == true;
        var subscriptionId = msg.ExternalReference != null ? msg.ExternalReference!.Replace("upgrade:","") : msg.SubscriptionAsaasId;

        logger.LogInformation(
            "Processing PaymentCreatedIntegrationEvent ({Type}) for PaymentAsaasId={PaymentAsaasId}, SubscriptionAsaasId={SubscriptionAsaasId}, Value={PaymentValue}",
            isUpgrade ? "UPGRADE" : "NORMAL", msg.PaymentAsaasId, subscriptionId, msg.PaymentValue
        );

        var subscription = await unitOfWork.Subscriptions
            .SingleOrDefaultAsync(new GetSubscriptionByAsaasIdSpecification(subscriptionId!));

        if (subscription == null)
        {
            logger.LogWarning("Subscription not found for AsaasId={SubscriptionAsaasId}", msg.SubscriptionAsaasId);
            return;
        }

        // evitar duplicação
        var exists = await unitOfWork.Payments
            .AnyAsync(new GetPaymentByAsaasIdSpecification(msg.PaymentAsaasId));
        if (exists) {
            logger.LogWarning("payment asaas with id={SubscriptionAsaasId} already exist.", msg.SubscriptionAsaasId);
            return;
        }

        // criar pagamento pendente no sistema
        subscription.AddPayment(
            msg.PaymentValue,
            msg.PaymentDueDate,
            (BillingTypeEnum) msg.PaymentBillingType,
            msg.PaymentAsaasId,
            msg.PaymentInvoiceUrl,
            $"Parcela {msg.PaymentDueDate.ToString("MM-yyyy")} - {msg.Description}"
        );

        await unitOfWork.CompleteAsync();

        // opcional: enviar email com link para pagamento
        var tenant = await unitOfWork.Tenants
            .SingleOrDefaultAsync(new GetTenantByIdGlobalSpecification(subscription.TenantId));

        if (tenant != null)
        {
            await emailService.SendPaymentLink(
                tenant.Contact.Email,
                tenant.Name,
                msg.PaymentInvoiceUrl,
                msg.PaymentValue,
                msg.PaymentDueDate
            );
            logger.LogInformation("Payment link email sent to {TenantEmail} for PaymentAsaasId={PaymentAsaasId}",
                tenant.Contact.Email, msg.PaymentAsaasId);
        }
        else
        {
            logger.LogWarning("Tenant not found for Subscription {SubscriptionId}", subscription.Id);
        }

        logger.LogInformation("PaymentCreatedIntegrationEvent successfully processed. PaymentAsaasId={PaymentAsaasId}", msg.PaymentAsaasId);
    }
}

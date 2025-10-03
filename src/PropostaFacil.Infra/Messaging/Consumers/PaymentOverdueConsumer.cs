using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Shared.Exceptions;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Payments.Specifications;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers;

public class PaymentOverdueConsumer(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    ILogger<PaymentOverdueConsumer> logger
) : IConsumer<PaymentOverdueIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PaymentOverdueIntegrationEvent> context)
    {
        var msg = context.Message;

        logger.LogInformation("Received PaymentOverdueIntegrationEvent for SubscriptionAsaasId={SubscriptionAsaasId}, PaymentAsaasId={PaymentAsaasId}, Value={PaymentValue}, DueDate={DueDate}",
            msg.SubscriptionAsaasId, msg.PaymentAsaasId, msg.Amount, msg.DueDate);

        var payment = await unitOfWork.Payments.SingleOrDefaultAsync(new GetPaymentByAsaasIdSpecification(msg.PaymentAsaasId));
        if (payment is null)
        {
            logger.LogWarning("Payment not found. PaymentAsaasId={PaymentAsaasId}", msg.PaymentAsaasId);
            return;
        }

        if (payment.Status == PaymentStatus.OVERDUE)
        {
            logger.LogWarning("Payment already overdue. PaymentAsaasId={PaymentAsaasId}", msg.PaymentAsaasId);
            return;
        }

        var subscription = await unitOfWork.Subscriptions.SingleOrDefaultAsync(new GetSubscriptionByAsaasIdSpecification(msg.SubscriptionAsaasId));

        if (subscription is null) throw new IntegrationException($"subscription asaas with id:{msg.SubscriptionAsaasId} not found.");

        if (subscription.Status == Domain.Enums.SubscriptionStatusEnum.Overdue)
        {
            logger.LogWarning("subscription already overdue. SubscriptionAsaasId={SubscriptionAsaasId}", msg.SubscriptionAsaasId);
            return;
        }

        await unitOfWork.BeginTransaction();

        subscription.Overdue();
        payment.Overdue();

        await unitOfWork.CompleteAsync();
        await unitOfWork.CommitAsync();

        try
        {
            await emailService.SendPaymentOverdue(
                subscription.Tenant.Contact.Email, subscription.Tenant.Name, msg.PaymentInvoiceUrl, msg.Amount, msg.DueDate
            );

            logger.LogInformation("Overdue payment email sent to {Email}", subscription.Tenant.Contact.Email);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send overdue payment email to {Email}", subscription.Tenant.Contact.Email);
        }
    }
}

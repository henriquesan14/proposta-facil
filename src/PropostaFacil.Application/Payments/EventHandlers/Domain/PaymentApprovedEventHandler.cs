using MassTransit;
using MediatR;
using PropostaFacil.Domain.Events;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Application.Payments.EventHandlers.Domain
{
    public class PaymentApprovedEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<PaymentApprovedEvent>
    {
        public async Task Handle(PaymentApprovedEvent notification, CancellationToken cancellationToken)
        {
            var payment = notification.Payment;
            var subscrition = notification.Subscription;
            var paymentApprovedIntegrationEvent = new PaymentApprovedIntegrationEvent(subscrition.Tenant.Contact.Email, subscrition.Tenant.Name, payment.Amount,
                payment.BillingType.ToString(), payment.PaidDate, subscrition.SubscriptionPlan.Name);

            await publishEndpoint.Publish(paymentApprovedIntegrationEvent);
        }
    }
}

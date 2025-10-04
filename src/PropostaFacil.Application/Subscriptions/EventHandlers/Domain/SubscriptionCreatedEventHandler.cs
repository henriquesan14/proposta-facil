using MassTransit;
using MediatR;
using PropostaFacil.Domain.Events;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Application.Subscriptions.EventHandlers.Domain;

public class SubscriptionCreatedEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<SubscriptionCreatedEvent>
{
    public async Task Handle(SubscriptionCreatedEvent subscriptionCreatedEvent, CancellationToken cancellationToken)
    {
        var subscription = subscriptionCreatedEvent.Subscription;
        var subscriptionCreatedIntegrationEvent = new SubscriptionCreatedIntegrationEvent(
            subscription.Id.Value, subscription.SubscriptionPlan.Price, subscription.SubscriptionPlan.Name, subscription.Tenant.Contact.Email, subscription.Tenant.Name, subscription.PaymentLink
        );
        await publishEndpoint.Publish(subscriptionCreatedIntegrationEvent, cancellationToken);
    }
}

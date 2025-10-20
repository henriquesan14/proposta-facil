using MassTransit;
using MediatR;
using PropostaFacil.Domain.Events;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Application.Users.EventHandlers.Domain;

public class UserSentEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent userCreatedEvent, CancellationToken cancellationToken)
    {
        var user = userCreatedEvent.User;
        var userCreatedIntegrationEvent = new UserCreatedIntegrationEvent(
            user.Contact.Email,
            user.Name,
            user.VerifiedToken!
        );
        await publishEndpoint.Publish(userCreatedIntegrationEvent, cancellationToken);
    }
}

using MassTransit;
using MediatR;
using PropostaFacil.Domain.Events;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Application.Users.EventHandlers.Domain;

public class ForgotPasswordEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<ForgotPasswordEvent>
{
    public async  Task Handle(ForgotPasswordEvent notification, CancellationToken cancellationToken)
    {
        var user = notification.User;
        var forgot = new ForgotPasswordIntegrationEvent(user.Id.Value, user.Contact.Email, user.Name, user.ForgottenToken!);
        await publishEndpoint.Publish(forgot, cancellationToken);
    }
}

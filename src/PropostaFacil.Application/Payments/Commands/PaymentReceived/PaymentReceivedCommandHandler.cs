using Common.ResultPattern;
using MassTransit;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Application.Payments.Commands.PaymentReceived;

public record PaymentReceivedCommandHandler(IPublishEndpoint publishEndpoint) : ICommandHandler<PaymentReceivedCommand, Result>
{
    public async Task<Result> Handle(PaymentReceivedCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payment;
        if (request.Event != "PAYMENT_RECEIVED")
            return PaymentErrors.InvalidEvent();

        await publishEndpoint.Publish(new PaymentReceivedIntegrationEvent(
                payload.Id,
                payload.Subscription,
                payload.Value,
                payload.PaymentDate,
                payload.DueDate
            ), cancellationToken);

        return Result.Success();
    }
}

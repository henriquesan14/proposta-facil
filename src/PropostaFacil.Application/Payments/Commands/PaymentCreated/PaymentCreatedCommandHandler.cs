using Common.ResultPattern;
using MassTransit;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Application.Payments.Commands.PaymentCreated;

public class PaymentCreatedCommandHandler(IPublishEndpoint publishEndpoint) : ICommandHandler<PaymentCreatedCommand, Result>
{
    public async Task<Result> Handle(PaymentCreatedCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payment;
        if (request.Event != "PAYMENT_CREATED")
            return PaymentErrors.InvalidEvent();

        await publishEndpoint.Publish(new PaymentCreatedIntegrationEvent(
                payload.Subscription,
                payload.Id,
                payload.Value,
                payload.DueDate,
                (int) payload.BillingType,
                payload.InvoiceUrl,
                payload.ExternalReference,
                payload.Description
            ), cancellationToken);

        return Result.Success();
    }
}

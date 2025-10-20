using Common.ResultPattern;
using MassTransit;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Application.Payments.Commands.PaymentOverdue;

public class PaymentOverdueCommandHandler(IPublishEndpoint publishEndpoint) : ICommandHandler<PaymentOverdueCommand, Result>
{
    public async Task<Result> Handle(PaymentOverdueCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payment;
        if (request.Event != "PAYMENT_OVERDUE")
            return PaymentErrors.InvalidEvent();

        await publishEndpoint.Publish(new PaymentOverdueIntegrationEvent(
                payload.Id,
                payload.Subscription,
                payload.Value,
                payload.PaymentDate,
                payload.DueDate,
                payload.InvoiceUrl
            ), cancellationToken);

        return Result.Success();
    }
}

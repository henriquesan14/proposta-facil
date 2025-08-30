using Common.ResultPattern;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Payments.Commands.ConfirmPaymentSubscription
{
    public record ConfirmPaymentSubscriptionCommand(string @Event, PaymentAsaas Payment) : ICommand<Result>;

    public record PaymentAsaas(
        string Id,
        string Customer,
        string Subscription,
        decimal Value,
        BillingTypeEnum BillingType,
        DateOnly PaymentDate,
        string InvoiceUrl
    );
}

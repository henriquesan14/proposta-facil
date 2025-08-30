using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Commands.ActivateSubscription
{
    public record ActivateSubscriptionCommand(string @Event, Payment Payment) : ICommand<Result>;

    public record Payment(
        string Id,
        string Customer,
        string Subscription,
        int Value,
        string BillingType,
        string Status
    );
}

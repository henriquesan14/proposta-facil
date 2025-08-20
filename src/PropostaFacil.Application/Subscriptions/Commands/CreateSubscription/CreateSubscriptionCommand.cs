using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Commands.CreateSubscription
{
    public record CreateSubscriptionCommand : ICommand<ResultT<SubscriptionResponse>>;
}

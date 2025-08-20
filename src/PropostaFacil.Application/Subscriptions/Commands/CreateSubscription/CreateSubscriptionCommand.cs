using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Commands.CreateSubscription
{
    public record CreateSubscriptionCommand(Guid TenantId, Guid SubscriptionPlanId, DateTime StartDate, DateTime? EndDate) : ICommand<ResultT<SubscriptionResponse>>;
}

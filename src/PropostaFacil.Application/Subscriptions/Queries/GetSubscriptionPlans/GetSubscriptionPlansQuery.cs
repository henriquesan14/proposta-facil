using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionPlans
{
    public record GetSubscriptionPlansQuery(string? Name) : IQuery<ResultT<IEnumerable<SubscriptionPlanResponse>>>;
}

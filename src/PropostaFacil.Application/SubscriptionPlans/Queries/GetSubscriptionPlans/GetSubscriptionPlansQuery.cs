using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.SubscriptionPlans.Queries.GetSubscriptionPlans;

public record GetSubscriptionPlansQuery(string? Name) : IQuery<ResultT<IEnumerable<SubscriptionPlanResponse>>>;

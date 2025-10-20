using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.SubscriptionPlans.Queries.GetSubscriptionPlanById;

public record GetSubscriptionPlanByIdQuery(Guid Id) : IQuery<ResultT<SubscriptionPlanResponse>>;


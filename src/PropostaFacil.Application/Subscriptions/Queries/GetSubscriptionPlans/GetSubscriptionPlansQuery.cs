using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionPlans
{
    public record GetSubscriptionPlansQuery(string? Name, int PageIndex = 1, int PageSize = 20) : IQuery<ResultT<PaginatedResult<SubscriptionPlanResponse>>>;
}

using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionPlans
{
    public class CachedGetSubscriptionPlansQueryHandler(IQueryHandler<GetSubscriptionPlansQuery, ResultT<PaginatedResult<SubscriptionPlanResponse>>> inner,
        ICacheService memoryCacheService) : IQueryHandler<GetSubscriptionPlansQuery, ResultT<PaginatedResult<SubscriptionPlanResponse>>>
    {
        public async Task<ResultT<PaginatedResult<SubscriptionPlanResponse>>> Handle(GetSubscriptionPlansQuery request, CancellationToken cancellationToken)
        {
            var nameKey = request.Name?.ToLower() ?? "all";
            var pageKey = $"{request.PageIndex}:{request.PageSize}";
            var cacheKey = $"SubscriptionPlans:{nameKey}:{pageKey}";

            var cached = await memoryCacheService.Get<PaginatedResult<SubscriptionPlanResponse>>(cacheKey);
            if (cached != null)
            {
                return cached;
            }

            var result = await inner.Handle(request, cancellationToken);

            await memoryCacheService.Set(cacheKey,
                result.Value,
                TimeSpan.FromMinutes(10)
            );

            return result;
        }
    }
}

using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionPlans
{
    public class CachedGetSubscriptionPlansQueryHandler(IQueryHandler<GetSubscriptionPlansQuery, ResultT<IEnumerable<SubscriptionPlanResponse>>> inner,
        ICacheService memoryCacheService) : IQueryHandler<GetSubscriptionPlansQuery, ResultT<IEnumerable<SubscriptionPlanResponse>>>
    {
        public async Task<ResultT<IEnumerable<SubscriptionPlanResponse>>> Handle(GetSubscriptionPlansQuery request, CancellationToken cancellationToken)
        {
            var nameKey = request.Name?.ToLower() ?? "all";
            var cacheKey = $"SubscriptionPlans:{nameKey}";

            var cached = await memoryCacheService.Get<IEnumerable<SubscriptionPlanResponse>>(cacheKey);
            if (cached != null)
            {
                return cached.ToList();
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

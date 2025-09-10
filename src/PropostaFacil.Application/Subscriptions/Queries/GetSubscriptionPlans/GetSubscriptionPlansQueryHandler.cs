using Common.ResultPattern;
using Microsoft.Extensions.Caching.Distributed;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;
using System.Text.Json;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionPlans
{
    public class GetSubscriptionPlansQueryHandler(IUnitOfWork unitOfWork, IDistributedCache cache) : IQueryHandler<GetSubscriptionPlansQuery, ResultT<PaginatedResult<SubscriptionPlanResponse>>>
    {
        public async Task<ResultT<PaginatedResult<SubscriptionPlanResponse>>> Handle(GetSubscriptionPlansQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.SubscriptionPlans.GetAllByNameAsync(
                request.Name!,
                request.PageNumber,
                request.PageSize
            );

            var subscriptionPlans = result.ToDto();
            var count = await unitOfWork.SubscriptionPlans.GetCountByNameAsync(request.Name!);

            var paginated = new PaginatedResult<SubscriptionPlanResponse>(
                request.PageNumber,
                request.PageSize,
                count,
                subscriptionPlans
            );

            return paginated;
        }
    }
}

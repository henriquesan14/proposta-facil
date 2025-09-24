using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionPlans
{
    public class GetSubscriptionPlansQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubscriptionPlansQuery, ResultT<PaginatedResult<SubscriptionPlanResponse>>>
    {
        public async Task<ResultT<PaginatedResult<SubscriptionPlanResponse>>> Handle(GetSubscriptionPlansQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.SubscriptionPlans.GetAllByNameAsync(
                request.Name!,
                request.PageIndex,
                request.PageSize
            );

            var subscriptionPlans = result.ToDto();
            var count = await unitOfWork.SubscriptionPlans.GetCountByNameAsync(request.Name!);

            var paginated = new PaginatedResult<SubscriptionPlanResponse>(
                request.PageIndex,
                request.PageSize,
                count,
                subscriptionPlans
            );

            return paginated;
        }
    }
}

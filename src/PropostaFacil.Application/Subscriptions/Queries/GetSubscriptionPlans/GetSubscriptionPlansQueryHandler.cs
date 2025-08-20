using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;
using System.Linq.Expressions;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionPlans
{
    public class GetSubscriptionPlansQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubscriptionPlansQuery, ResultT<PaginatedResult<SubscriptionPlanResponse>>>
    {
        public async Task<ResultT<PaginatedResult<SubscriptionPlanResponse>>> Handle(GetSubscriptionPlansQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<SubscriptionPlan, bool>> predicate = p =>
                (string.IsNullOrEmpty(request.Name) ||
                    p.Name.ToLower().Contains(request.Name.ToLower()));
            var subscriptionPlans = await unitOfWork.SubscriptionPlans.GetAsync(predicate);
            var count = await unitOfWork.SubscriptionPlans.GetCountAsync(predicate);

            var dto = subscriptionPlans.ToDto();

            var paginated = new PaginatedResult<SubscriptionPlanResponse>(
                request.PageNumber,
                request.PageSize,
                count,
                dto
            );

            return paginated;
        }
    }
}

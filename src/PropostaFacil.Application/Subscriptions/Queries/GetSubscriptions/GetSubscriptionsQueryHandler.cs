using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;
using System.Linq.Expressions;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptions
{
    public class GetSubscriptionsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubscriptionsQuery, ResultT<PaginatedResult<SubscriptionResponse>>>
    {
        public async Task<ResultT<PaginatedResult<SubscriptionResponse>>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Subscription, bool>> predicate = s =>
            (!request.TenantId.HasValue || s.TenantId == TenantId.Of(request.TenantId.Value)) &&
            (!request.SubscriptionPlanId.HasValue || s.SubscriptionPlanId == SubscriptionPlanId.Of(request.SubscriptionPlanId.Value)) &&
            (!request.Status.HasValue || s.Status == request.Status.Value) &&
            (!request.StartDate.HasValue || s.StartDate.Date >= request.StartDate.Value.Date) &&
            (!request.EndDate.HasValue || s.EndDate.HasValue && s.EndDate.Value.Date <= request.EndDate.Value.Date);
            List<Expression<Func<Subscription, object>>> includes = new List<Expression<Func<Subscription, object>>>()
            {
                s => s.SubscriptionPlan
            };


            var subscriptions = await unitOfWork.Subscriptions.GetAsync(predicate, includes: includes);
            var count = await unitOfWork.Subscriptions.GetCountAsync(predicate);

            var dto = subscriptions.ToDto();

            var paginated = new PaginatedResult<SubscriptionResponse>(
                request.PageNumber,
                request.PageSize,
                count,
                dto
            );

            return paginated;

        }
    }
}

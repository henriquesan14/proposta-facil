using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptions
{
    public class GetSubscriptionsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubscriptionsQuery, ResultT<PaginatedResult<SubscriptionResponse>>>
    {
        public async Task<ResultT<PaginatedResult<SubscriptionResponse>>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
          var spec = new ListSubscriptionsSpecification(request.TenantName, request.SubscriptionPlanId, request.Status, request.StartDate, request.EndDate);
            var paginated = await unitOfWork.Subscriptions.ToPaginatedListAsync(spec, request.PageIndex, request.PageSize, s => s.ToDto());

            return paginated;
        }
    }
}

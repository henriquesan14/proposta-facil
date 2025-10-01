using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.SubscriptionPlans.Specifications;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.SubscriptionPlans.Queries.GetSubscriptionPlans
{
    public class GetSubscriptionPlansQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubscriptionPlansQuery, ResultT<IEnumerable<SubscriptionPlanResponse>>>
    {
        public async Task<ResultT<IEnumerable<SubscriptionPlanResponse>>> Handle(GetSubscriptionPlansQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.SubscriptionPlans.ListAsync(new ListSubscriptionPlansGlobalSpecification(request.Name));

            return result.ToDto();
        }
    }
}

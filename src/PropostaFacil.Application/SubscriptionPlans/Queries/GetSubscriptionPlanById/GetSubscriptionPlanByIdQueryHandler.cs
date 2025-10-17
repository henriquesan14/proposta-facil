using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.SubscriptionPlans.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.SubscriptionPlans.Queries.GetSubscriptionPlanById;

public class GetSubscriptionPlanByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubscriptionPlanByIdQuery, ResultT<SubscriptionPlanResponse>>
{
    public async Task<ResultT<SubscriptionPlanResponse>> Handle(GetSubscriptionPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = await unitOfWork.SubscriptionPlans.SingleOrDefaultAsync(new GetSubscriptionPlanByIdGlobalSpecification(SubscriptionPlanId.Of(request.Id)));
        if (subscriptionPlan == null) return SubscriptionPlanErrors.NotFound(request.Id);

        return subscriptionPlan.ToDto();
    }
}

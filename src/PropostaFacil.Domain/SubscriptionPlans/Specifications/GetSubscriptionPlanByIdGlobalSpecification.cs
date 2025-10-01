using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.SubscriptionPlans.Specifications;

public class GetSubscriptionPlanByIdGlobalSpecification : GlobalSingleResultSpecification<SubscriptionPlan, SubscriptionPlanId>
{
    public GetSubscriptionPlanByIdGlobalSpecification(SubscriptionPlanId id)
    {
        Query
            .Where(sp => sp.Id == id && sp.IsActive);
    }
}

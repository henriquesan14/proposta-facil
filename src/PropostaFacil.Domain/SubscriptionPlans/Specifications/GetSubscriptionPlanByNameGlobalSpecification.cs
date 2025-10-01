using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.SubscriptionPlans.Specifications;

public class GetSubscriptionPlanByNameGlobalSpecification : GlobalSingleResultSpecification<SubscriptionPlan, SubscriptionPlanId>
{
    public GetSubscriptionPlanByNameGlobalSpecification(string? name)
    {
        Query
            .Where(sp => (string.IsNullOrEmpty(name) || sp.Name.ToLower() == name.ToLower()));
    }
}

using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using System.Data;

namespace PropostaFacil.Domain.SubscriptionPlans.Specifications;

public class ListSubscriptionPlansGlobalSpecification : GlobalSpecification<SubscriptionPlan, SubscriptionPlanId>
{
    public ListSubscriptionPlansGlobalSpecification(string? name, bool onlyActive)
    {
        Query
            .Where(sp =>
                (!onlyActive || sp.IsActive) &&
                (string.IsNullOrEmpty(name) ||
                    sp.Name.ToLower().Contains(name.ToLower())))
            .OrderBy(sp => sp.Price);
    }
}

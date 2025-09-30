using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using System.Data;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class ListSubscriptionPlansGlobalSpecification : GlobalSpecification<SubscriptionPlan, SubscriptionPlanId>
{
    public ListSubscriptionPlansGlobalSpecification(string? name)
    {
        Query
            .Where(sp => (string.IsNullOrEmpty(name) ||
                    sp.Name.ToLower().Contains(name.ToLower())))
            .OrderBy(sp => sp.Name);
    }
}

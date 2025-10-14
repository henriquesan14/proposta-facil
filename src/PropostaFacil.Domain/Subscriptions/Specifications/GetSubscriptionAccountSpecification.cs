using Ardalis.Specification;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class GetSubscriptionAccountSpecification : SingleResultSpecification<Subscription>
{
    public GetSubscriptionAccountSpecification()
    {
        Query
            .Where(s => s.IsActive)
            .Include(s => s.SubscriptionPlan)
            .Include(s => s.PendingUpgradePlan)
            .Include(s => s.Tenant);
    }
}

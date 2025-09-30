using Ardalis.Specification;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class GetSubscriptionsByTenantSpecification : Specification<Subscription>
{
    public GetSubscriptionsByTenantSpecification()
    {
        Query
            .Include(s => s.SubscriptionPlan)
            .Include(s => s.Tenant)
            .Include(s => s.Payments);
    }
}

using Ardalis.Specification;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class GetSubscriptionByAsaasIdSpecification : Specification<Subscription>
{
    public GetSubscriptionByAsaasIdSpecification(string subscriptionId)
    {
        Query.Where(s => s.SubscriptionAsaasId == subscriptionId)
            .Include(s => s.SubscriptionPlan)
            .Include(s => s.Tenant);
    }
}

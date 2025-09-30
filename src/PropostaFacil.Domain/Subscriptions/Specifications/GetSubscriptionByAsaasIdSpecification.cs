using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class GetSubscriptionByAsaasIdSpecification : GlobalSingleResultSpecification<Subscription, SubscriptionId>
{
    public GetSubscriptionByAsaasIdSpecification(string subscriptionId)
    {
        Query.Where(s => s.SubscriptionAsaasId == subscriptionId)
            .Include(s => s.SubscriptionPlan)
            .Include(s => s.Tenant);
    }
}

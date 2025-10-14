using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class GetSubscriptionByTenantSpecification : GlobalSingleResultSpecification<Subscription, SubscriptionId>
{
    public GetSubscriptionByTenantSpecification(TenantId tenantId)
    {
        Query
            .Where(s => s.TenantId == tenantId && s.IsActive);
    }
}

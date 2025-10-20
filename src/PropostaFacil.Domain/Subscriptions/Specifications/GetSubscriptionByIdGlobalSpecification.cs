using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class GetSubscriptionByIdGlobalSpecification : GlobalSingleResultSpecification<Subscription, SubscriptionId>
{
    public GetSubscriptionByIdGlobalSpecification(SubscriptionId id)
    {
        Query
            .Where(s => s.Id == id && s.IsActive)
            .Include(s => s.SubscriptionPlan)
            .Include(s => s.Tenant);
    }
}

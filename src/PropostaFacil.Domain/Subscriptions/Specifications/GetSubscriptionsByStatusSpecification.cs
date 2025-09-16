using Ardalis.Specification;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class GetSubscriptionsByStatusSpecification : SingleResultSpecification<Subscription>
{
    public GetSubscriptionsByStatusSpecification(SubscriptionStatusEnum status)
    {
        Query
            .Where(s => s.Status == status)
            .Include(s => s.SubscriptionPlan);
    }
}

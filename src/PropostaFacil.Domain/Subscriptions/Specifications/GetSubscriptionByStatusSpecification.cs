using Ardalis.Specification;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class GetSubscriptionByStatusSpecification : SingleResultSpecification<Subscription>
{
    public GetSubscriptionByStatusSpecification(SubscriptionStatusEnum status)
    {
        Query
            .Where(s => s.Status == status)
            .Include(s => s.SubscriptionPlan);
    }
}

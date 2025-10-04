using Ardalis.Specification;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class GetOverdueSubscriptionsSpecification : GlobalSpecification<Subscription, SubscriptionId>
{
    public GetOverdueSubscriptionsSpecification()
    {
        Query.Where(s => s.Status == SubscriptionStatusEnum.Overdue)
            .Include(s => s.Tenant);
    }
}

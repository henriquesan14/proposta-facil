using Ardalis.Specification;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class ListSubscriptionsSpecification : Specification<Subscription>
{
    public ListSubscriptionsSpecification(Guid? subscriptionPlanId, SubscriptionStatusEnum? status, DateTime? startDate, DateTime? endDate)
    {
        Query
            .Where(s =>
                (!subscriptionPlanId.HasValue || s.SubscriptionPlanId == SubscriptionPlanId.Of(subscriptionPlanId.Value)) &&
                (!status.HasValue || s.Status == status.Value) &&
                (!startDate.HasValue || s.StartDate.Date >= startDate.Value.Date) &&
                (!endDate.HasValue || s.EndDate.HasValue && s.EndDate.Value.Date <= endDate.Value.Date)
            )
            .Include(s => s.SubscriptionPlan);
    }
}

using Ardalis.Specification;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Subscriptions.Specifications;

public class ListSubscriptionsSpecification : GlobalSpecification<Subscription, SubscriptionId>
{
    public ListSubscriptionsSpecification(string? tenantName, Guid? subscriptionPlanId, SubscriptionStatusEnum? status, DateTime? startDate, DateTime? endDate, bool onlyActive)
    {
        Query
            .Where(s =>
                (!onlyActive || s.IsActive) &&
                (string.IsNullOrEmpty(tenantName) || s.Tenant.Name.ToLower().Contains(tenantName.ToLower())) &&
                (!subscriptionPlanId.HasValue || s.SubscriptionPlanId == SubscriptionPlanId.Of(subscriptionPlanId.Value)) &&
                (!status.HasValue || s.Status == status.Value) &&
                (!startDate.HasValue || (s.StartDate.HasValue && s.StartDate.Value.Date >= startDate.Value.Date)) &&
                (!endDate.HasValue || (s.StartDate.HasValue && s.StartDate.Value.Date <= endDate.Value.Date))
            )
            .Include(s => s.SubscriptionPlan)
            .Include(s => s.Tenant);
    }
}

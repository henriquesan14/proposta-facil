using PropostaFacil.Domain.Entities;

namespace PropostaFacil.Application.Subscriptions
{
    public static class SubscriptionExtensions
    {
        public static SubscriptionResponse ToDto(this Subscription subscription)
        {
            return new SubscriptionResponse(
                subscription.Id.Value,
                subscription.TenantId.Value,
                subscription.SubscriptionPlanId.Value,
                subscription.SubscriptionPlan?.ToDto()!,
                subscription.StartDate,
                subscription.EndDate,
                subscription.Status,
                subscription.ProposalsUsed
            );
        }

        public static List<SubscriptionResponse> ToDto(this IEnumerable<Subscription> subscriptions)
        {
            return subscriptions
                .Select(ToDto)
                .ToList();
        }
    }
}

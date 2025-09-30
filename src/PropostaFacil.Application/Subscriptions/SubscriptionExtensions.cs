using PropostaFacil.Application.Payments;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Domain.Subscriptions;

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
                subscription.Status,
                subscription.ProposalsUsed,
                subscription.SubscriptionAsaasId,
                subscription.PaymentLink,
                subscription.Tenant?.ToDto()!,
                subscription.Payments.ToDto()
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

using PropostaFacil.Domain.Entities;

namespace PropostaFacil.Application.Subscriptions
{
    public static class SubscriptionPlanExtensions
    {
        public static SubscriptionPlanResponse ToDto(this SubscriptionPlan subscriptionPlan)
        {
            return new SubscriptionPlanResponse(
                subscriptionPlan.Id.Value,
                subscriptionPlan.Name,
                subscriptionPlan.MaxProposalsPerMonth,
                subscriptionPlan.Price,
                subscriptionPlan.Description
            );
        }

        public static List<SubscriptionPlanResponse> ToDto(this IEnumerable<SubscriptionPlan> subscriptionPlans)
        {
            return subscriptionPlans
                .Select(ToDto)
                .ToList();
        }
    }
}

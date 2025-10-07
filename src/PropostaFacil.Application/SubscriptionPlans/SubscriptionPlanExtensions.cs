using PropostaFacil.Domain.SubscriptionPlans;

namespace PropostaFacil.Application.SubscriptionPlans;

public static class SubscriptionPlanExtensions
{
    public static SubscriptionPlanResponse ToDto(this SubscriptionPlan subscriptionPlan)
    {
        return new SubscriptionPlanResponse(
            subscriptionPlan.Id.Value,
            subscriptionPlan.Name,
            subscriptionPlan.MaxProposalsPerMonth,
            subscriptionPlan.Price,
            subscriptionPlan.Description,
            subscriptionPlan.IsActive,
            subscriptionPlan.CreatedAt
        );
    }

    public static List<SubscriptionPlanResponse> ToDto(this IEnumerable<SubscriptionPlan> subscriptionPlans)
    {
        return subscriptionPlans
            .Select(ToDto)
            .ToList();
    }
}

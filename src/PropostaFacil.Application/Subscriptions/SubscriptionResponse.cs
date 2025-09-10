using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Application.Subscriptions
{
    public record SubscriptionResponse(Guid Id, Guid TenantId, Guid SubscriptionPlanId, SubscriptionPlanResponse SubscriptionPlan, DateTime StartDate, DateTime? EndDate,
        SubscriptionStatusEnum Status, int ProposalsUsed, string SubscriptionAsaasId, string paymentLink);
}

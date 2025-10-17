using PropostaFacil.Application.Payments;
using PropostaFacil.Application.SubscriptionPlans;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Application.Subscriptions;

public record SubscriptionResponse(Guid Id, Guid TenantId, Guid SubscriptionPlanId, SubscriptionPlanResponse SubscriptionPlan, SubscriptionPlanResponse PendingUpgradePlan, DateTime? StartDate,
    SubscriptionStatusEnum Status, int ProposalsUsed, string SubscriptionAsaasId, string paymentLink, TenantResponse Tenant, IEnumerable<PaymentResponse> Payments,
    bool IsActive, DateTime? CreatedAt, string? CreatedByName);

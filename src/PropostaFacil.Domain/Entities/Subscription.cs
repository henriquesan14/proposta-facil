using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Entities
{
    public class Subscription : Aggregate<SubscriptionId>
    {
        public static Subscription Create(TenantId tenantId, SubscriptionPlanId subscriptionPlanId, DateTime startDate, DateTime? endDate = null)
        {
            return new Subscription
            {
                Id = SubscriptionId.Of(Guid.NewGuid()),
                TenantId = tenantId,
                SubscriptionPlanId = subscriptionPlanId,
                StartDate = startDate,
                EndDate = endDate,
                Status = SubscriptionStatusEnum.Active,
                ProposalsUsed = 0
            };
        }

        public TenantId TenantId { get; private set; } = default!;
        public SubscriptionPlanId SubscriptionPlanId { get; private set; } = default!;
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public SubscriptionStatusEnum Status { get; private set; }
        public int ProposalsUsed { get; private set; }

        public Tenant Tenant { get; private set; } = default!;
        public SubscriptionPlan SubscriptionPlan { get; private set; } = default!;

        public void IncrementProposalsUsed()
        {
            ProposalsUsed++;
        }

        public void Cancel() => Status = SubscriptionStatusEnum.Canceled;
        public void Suspend() => Status = SubscriptionStatusEnum.Suspended;
    }
}

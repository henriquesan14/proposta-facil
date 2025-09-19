using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Events;
using PropostaFacil.Domain.Payments;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Subscriptions
{
    public class Subscription : Aggregate<SubscriptionId>
    {
        private readonly List<Payment> _payments = new();
        public static Subscription Create(TenantId tenantId, SubscriptionPlanId subscriptionPlanId, DateTime startDate, string subscriptionAsaasId, string paymentLink, DateTime? endDate = null)
        {
            var subscription =  new Subscription
            {
                Id = SubscriptionId.Of(Guid.NewGuid()),
                TenantId = tenantId,
                SubscriptionPlanId = subscriptionPlanId,
                StartDate = startDate,
                EndDate = endDate,
                Status = SubscriptionStatusEnum.Pending,
                ProposalsUsed = 0,
                SubscriptionAsaasId = subscriptionAsaasId,
                PaymentLink = paymentLink
            };
            subscription.AddDomainEvent(new SubscriptionCreatedEvent(subscription));
            return subscription;
        }

        public TenantId TenantId { get; private set; } = default!;
        public SubscriptionPlanId SubscriptionPlanId { get; private set; } = default!;
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public SubscriptionStatusEnum Status { get; private set; }
        public int ProposalsUsed { get; private set; }
        public string SubscriptionAsaasId { get; private set; } = default!;
        public string PaymentLink { get; private set; } = default!;

        public Tenant Tenant { get; private set; } = default!;
        public SubscriptionPlan SubscriptionPlan { get; private set; } = default!;

        public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

        public void IncrementProposalsUsed()
        {
            ProposalsUsed++;
        }

        public void ResetProposalsUsed()
        {
            ProposalsUsed = 0;
        }

        public void AddPayment(decimal amount, DateOnly paidDate, BillingTypeEnum billingType, string paymentAsaasId, string paymentLink, bool isFirstInvoice = false)
        {
            var payment = Payment.Create(amount, paidDate, billingType, paymentAsaasId, paymentLink, isFirstInvoice);
            payment.SetSubscription(Id);

            _payments.Add(payment);
            AddDomainEvent(new PaymentApprovedEvent(payment, this));
        }

        public void Activate() => Status = SubscriptionStatusEnum.Active;
        public void Cancel() => Status = SubscriptionStatusEnum.Canceled;
        public void Suspend() => Status = SubscriptionStatusEnum.Suspended;
    }
}

using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Events;
using PropostaFacil.Domain.Proposals;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Payments
{
    public class Payment : Aggregate<PaymentId>
    {
        public SubscriptionId? SubscriptionId { get; private set; } = default!;
        public ProposalId? ProposalId { get; private set; } = default!;
        public decimal Amount { get; private set; } = default!;
        public string Currency { get; private set; } = "BRL";
        public BillingTypeEnum BillingType { get; private set; } = default!;
        public PaymentStatus Status { get; private set; } = default!;
        public DateOnly? PaymentDate { get; private set; } = default!;
        public DateOnly DueDate { get; private set; } = default!;
        public string PaymentAsaasId { get; private set; } = default!;
        public string PaymentLink { get; private set; } = default!;
        public Subscription? Subscription { get; private set; } = default!;
        public Proposal? Proposal { get; private set; } = default!;

        public static Payment Create(decimal amount, DateOnly dueDate, BillingTypeEnum billingType, string paymentAsaasId, string paymentLink, string currency = "BRL")
        {
            return new Payment {
                Id = PaymentId.Of(Guid.NewGuid()),
                Amount = amount,
                DueDate = dueDate,
                Currency = currency,
                BillingType = billingType,
                PaymentAsaasId = paymentAsaasId,
                PaymentLink = paymentLink,
                Status = PaymentStatus.PENDING
            };
        }

        public void ConfirmSubscriptionPayment(DateOnly paymentDate)
        {
            PaymentDate = paymentDate;
            Status = PaymentStatus.RECEIVED;
        }

        public void SetSubscription(SubscriptionId subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }

        public void SetProposal(ProposalId proposalId)
        {
            ProposalId = proposalId;
        }
    }
}

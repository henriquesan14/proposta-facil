using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Entities
{
    public class Payment : Aggregate<PaymentId>
    {
        public SubscriptionId? SubscriptionId { get; private set; } = default!;
        public ProposalId? ProposalId { get; private set; } = default!;
        public decimal Amount { get; private set; } = default!;
        public string Currency { get; private set; } = "BRL";
        public BillingTypeEnum BillingType { get; private set; } = default!;
        public DateOnly PaidDate { get; private set; } = default!;
        public string PaymentAsaasId { get; private set; } = default!;
        public string PaymentLink { get; private set; } = default!;
        public Subscription? Subscription { get; private set; } = default!;
        public Proposal? Proposal { get; private set; } = default!;

        public bool IsFirstInvoice { get; private set; } = default!;

        public static Payment Create(decimal amount, DateOnly paidDate, BillingTypeEnum billingType, string paymentAsaasId, string paymentLink, bool isFirstInvoice, string currency = "BRL")
        {
            return new Payment {
                Id = PaymentId.Of(Guid.NewGuid()),
                Amount = amount,
                PaidDate = paidDate,
                Currency = currency,
                BillingType = billingType,
                PaymentAsaasId = paymentAsaasId,
                PaymentLink = paymentLink,
                IsFirstInvoice = isFirstInvoice
            };
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

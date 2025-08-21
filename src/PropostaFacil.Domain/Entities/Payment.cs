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
        public PaymentStatus Status { get; private set; } = default!;
        public DateTime DueDate { get; private set; } = default!;
        public DateTime? PaidDate { get; private set; } = default!;
        public string ExternalReference { get; private set; } = default!;

        public Subscription? Subscription { get; private set; } = default!;
        public Proposal? Proposal { get; private set; } = default!;

        public static Payment Create(decimal amount, DateTime dueDate, BillingTypeEnum billingType, string currency = "BRL")
        {
            return new Payment {
                Id = PaymentId.Of(Guid.NewGuid()),
                Amount = amount,
                DueDate = dueDate,
                Currency = currency,
                BillingType = billingType,
                Status = PaymentStatus.Pending
            };
        }

        public void MarkAsPaid(DateTime paidDate)
        {
            Status = PaymentStatus.Paid;
            PaidDate = paidDate;
        }

        public void MarkAsFailed()
        {
            Status = PaymentStatus.Failed;
        }
    }
}

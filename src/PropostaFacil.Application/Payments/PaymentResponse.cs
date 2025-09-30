using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Application.Payments;

public record PaymentResponse(Guid Id, Guid? SubscriptionId, Guid? ProposalId, decimal Amount, string Currency, BillingTypeEnum BillingType, DateOnly PaidDate, string PaymentAsaasId, string PaymentLink);

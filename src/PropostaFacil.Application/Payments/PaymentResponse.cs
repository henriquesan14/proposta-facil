using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Application.Payments;

public record PaymentResponse(Guid Id, Guid? SubscriptionId, Guid? ProposalId, PaymentStatus Status, decimal Amount, string Currency, BillingTypeEnum BillingType, DateOnly? PaymentDate, DateOnly DueDate, string PaymentAsaasId, string PaymentLink, string? Description);

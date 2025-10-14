using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Application.Payments;

public record PaymentAsaas(
    string Id,
    string Customer,
    string Subscription,
    decimal Value,
    BillingTypeEnum BillingType,
    DateOnly? PaymentDate,
    DateOnly? ConfirmedDate,
    DateOnly DueDate,
    string InvoiceUrl,
    string? ExternalReference,
    string? Description
);

namespace PropostaFacil.Shared.Messaging.Events;

public record PaymentCreatedIntegrationEvent(
    string SubscriptionAsaasId,
    string PaymentAsaasId,
    decimal PaymentValue,
    DateOnly PaymentDueDate,
    int PaymentBillingType,
    string PaymentInvoiceUrl,
    string? ExternalReference,
    string? Description
) : IntegrationEvent;

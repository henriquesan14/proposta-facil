namespace PropostaFacil.Shared.Messaging.Events;

public record PaymentOverdueIntegrationEvent(string PaymentAsaasId, string SubscriptionAsaasId, decimal Amount,
        DateOnly? PaymentDate, DateOnly DueDate, string PaymentInvoiceUrl) :  IntegrationEvent;

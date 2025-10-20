namespace PropostaFacil.Shared.Messaging.Events;

public record PaymentReceivedIntegrationEvent(string PaymentAsaasId, string SubscriptionAsaasId, decimal Amount,
    DateOnly? PaymentDate, DateOnly? ConfirmedDate, DateOnly DueDate, string? ExternalReference) : IntegrationEvent;

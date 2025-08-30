namespace PropostaFacil.Shared.Messaging.Events
{
    public record SubscriptionCreatedIntegrationEvent(
        Guid SubscriptionId,
        decimal PlanPrice,
        string PlanName,
        string ClientEmail,
        string ClientName,
        string PaymentLink
    ) : IntegrationEvent;
}

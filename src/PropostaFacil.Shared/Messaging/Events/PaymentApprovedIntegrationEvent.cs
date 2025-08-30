namespace PropostaFacil.Shared.Messaging.Events
{
    public record PaymentApprovedIntegrationEvent(string ClientEmail, string ClientName, decimal Amount,
        string BillingType, DateOnly PaidDate, string PlanName) : IntegrationEvent;
}

namespace PropostaFacil.Shared.Messaging.Events;

public record ProposalSentIntegrationEvent(
    Guid ProposalId,
    Guid TenantId,
    Guid ClientId,
    string ProposalNumber,
    string ProposalTitle,
    decimal TotalAmount,
    string Currency,
    string ClientEmail,
    string ClientName,
    DateTime ValidUntil,
    IEnumerable<ProposalItemIntegrationEvent> Items
) : IntegrationEvent;

public record ProposalItemIntegrationEvent(
    string Name,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);

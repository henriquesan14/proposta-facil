namespace PropostaFacil.Shared.Messaging.Events;

public record UserCreatedIntegrationEvent(string email, string name, string verifiedToken) : IntegrationEvent;


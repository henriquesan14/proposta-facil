namespace PropostaFacil.Shared.Messaging.Events;

public record ForgotPasswordIntegrationEvent(Guid UserId, string Email, string Name, string ForgotToken) : IntegrationEvent;

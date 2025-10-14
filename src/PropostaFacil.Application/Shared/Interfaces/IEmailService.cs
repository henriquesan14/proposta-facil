using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Application.Shared.Interfaces;

public interface IEmailService
{
    Task SendVerifyEmailAddress(string email, string name, string verificationLink);
    Task SendForgotPassword(string email, string name, string resetPasswordLink);
    Task SendConfirmPayment(string email, string clientName, decimal amount, DateOnly? paymentDate, string planName);
    Task SendConfirmSubscription(string email, string clientName, string planName, decimal price, string paymentLink);
    Task SendProposal(string email, string proposalNumber, string clientName, DateTime validUntil, IEnumerable<ProposalItemIntegrationEvent> items, decimal totalAmount);
    Task SendPaymentLink(string email, string name, string paymentLink, decimal value, DateOnly dueDate);
    Task SendPaymentOverdue(string email, string name, string paymentLink, decimal value, DateOnly dueDate);
    Task SendSubscriptionExpired(string email, string name, string paymentLink, decimal value, DateOnly dueDate);
    Task SendConfirmUpgradePlan(string email, string clientName, string planName, decimal newPrice);
}

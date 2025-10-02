using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Users;
using PropostaFacil.Infra.Emails.Builders;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Emails;

public class EmailService(IEmailSender sender) : IEmailService
{
    public async Task SendVerifyEmailAddress(string email, string name, string verificationLink)
    {
        var html = UserEmailBuilder.BuildSendVerifyEmailAddress(name, verificationLink);
        await sender.SendEmailAsync(email, "Verificação de Email", html);
    }

    public async Task SendForgotPassword(User user)
    {
        var html = UserEmailBuilder.BuildSendForgotPassword(user.Name);
        await sender.SendEmailAsync(user.Contact.Email, "Esqueceu sua senha", html);
    }

    public async Task SendConfirmPayment(string email, string clientName, decimal amount, DateOnly paidDate, string planName)
    {
        var html = PaymentEmailBuilder.BuildConfirmPayment(clientName, amount, paidDate, planName);
        await sender.SendEmailAsync(email, "Seu pagamento foi aprovado", html);
    }

    public async Task SendConfirmSubscription(string email, string clientName, string planName, decimal price, string paymentLink)
    {
        var html = SubscriptionEmailBuilder.BuildConfirmSubscription(clientName, planName, price, paymentLink);
        await sender.SendEmailAsync(email, "Confirmação de assinatura", html);
    }

    public async Task SendProposal(string email, string proposalNumber, string clientName, DateTime validUntil, IEnumerable<ProposalItemIntegrationEvent> items, decimal totalAmount)
    {
        var html = ProposalEmailBuilder.BuildProposal(proposalNumber, clientName, validUntil, items, totalAmount);
        await sender.SendEmailAsync(email, "Proposta recebida", html);
    }
}

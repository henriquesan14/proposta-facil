using PropostaFacil.Application.Shared.Interfaces;
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

    public async Task SendForgotPassword(string email, string name, string resetPasswordLink)
    {
        var html = UserEmailBuilder.BuildSendForgotPassword(name, resetPasswordLink);
        await sender.SendEmailAsync(email, "Esqueceu sua senha", html);
    }

    public async Task SendConfirmPayment(string email, string clientName, decimal amount, DateOnly? paymentDate, string planName)
    {
        var html = PaymentEmailBuilder.BuildConfirmPayment(clientName, amount, paymentDate, planName);
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

    public async Task SendPaymentLink(string email, string name, string paymentLink, decimal value, DateOnly dueDate)
    {
        var html = PaymentEmailBuilder.BuildPaymentCreated(name, paymentLink, value, dueDate);
        await sender.SendEmailAsync(email, "Sua fatura de assinatura foi gerada", html);
    }

    public async Task SendPaymentOverdue(string email, string name, string paymentLink, decimal value, DateOnly dueDate)
    {
        var html = PaymentEmailBuilder.BuildPaymentOverdue(name, paymentLink, value, dueDate);
        await sender.SendEmailAsync(email, "Sua fatura de assinatura está vencida", html);
    }

    public async Task SendSubscriptionExpired(string email, string name, string paymentLink, decimal value, DateOnly dueDate)
    {
        var html = SubscriptionEmailBuilder.BuildSubscriptionExpired(name, paymentLink, value, dueDate);
        await sender.SendEmailAsync(email, "Sua assinatura expirou por falta de pagamento", html);
    }
}

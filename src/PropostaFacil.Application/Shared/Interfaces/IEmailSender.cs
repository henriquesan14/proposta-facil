using PropostaFacil.Application.Proposals;

namespace PropostaFacil.Application.Shared.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlBody);
    }
}

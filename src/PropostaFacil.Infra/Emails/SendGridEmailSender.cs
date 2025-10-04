using Microsoft.Extensions.Configuration;
using PropostaFacil.Application.Shared.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PropostaFacil.Infra.Emails;

public class SendGridEmailSender(IConfiguration configuration) : IEmailSender
{
    public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        var apiKey = configuration["EmailSettings:ApiKey"]!;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(configuration["EmailSettings:EmailFrom"]!, configuration["EmailSettings:NameFrom"]!);
        var subjectLine = subject;
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subjectLine, htmlBody, htmlBody);
        await client.SendEmailAsync(msg);
    }
}

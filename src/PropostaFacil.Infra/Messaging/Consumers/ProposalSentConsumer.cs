using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Proposals.Email;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers
{
    public class ProposalSentConsumer(
        IEmailSender emailSender,
        ILogger<ProposalSentConsumer> logger
    ) : IConsumer<ProposalSentIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProposalSentIntegrationEvent> context)
        {
            var msg = context.Message;

            var subject = $"Sua proposta #{msg.ProposalNumber}";

            var body = ProposalEmailBuilder.BuildHtml(msg.ProposalNumber, msg.ClientName, msg.ValidUntil, msg.Items, msg.TotalAmount);

            try
            {
                await emailSender.SendEmailAsync(
                    toEmail: msg.ClientEmail,
                    subject: subject,
                    htmlBody: body
                );

                logger.LogInformation("E-mail de proposta enviada. ProposalId={ProposalId}", msg.ProposalId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Falha ao enviar e-mail. ProposalId={ProposalId}", msg.ProposalId);
                throw;
            }
        }
    }
}

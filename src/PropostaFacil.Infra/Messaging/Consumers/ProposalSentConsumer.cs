using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers
{
    public class ProposalSentConsumer(
        IEmailService emailService,
        ILogger<ProposalSentConsumer> logger
    ) : IConsumer<ProposalSentIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProposalSentIntegrationEvent> context)
        {
            var msg = context.Message;
            try
            {
                await emailService.SendProposal(
                    msg.ClientEmail, msg.ProposalNumber, msg.ClientName, msg.ValidUntil, msg.Items, msg.TotalAmount
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

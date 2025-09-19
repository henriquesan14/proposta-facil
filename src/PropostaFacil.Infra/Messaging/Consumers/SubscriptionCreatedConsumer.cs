using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers
{
    public class SubscriptionCreatedConsumer(
        IEmailService emailService,
        ILogger<SubscriptionCreatedConsumer> logger
    ) : IConsumer<SubscriptionCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<SubscriptionCreatedIntegrationEvent> context)
        {
            var msg = context.Message;
            try
            {
                await emailService.SendConfirmSubscription(
                    msg.ClientEmail, msg.ClientName, msg.PlanName, msg.PlanPrice, msg.PaymentLink
                );

                logger.LogInformation("Confirmação de assinatura enviada. ProposalId={ProposalId}", msg.SubscriptionId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Falha ao enviar e-mail. Subscription={SubscriptionId}", msg.SubscriptionId);
                throw;
            }
        }
    }
}

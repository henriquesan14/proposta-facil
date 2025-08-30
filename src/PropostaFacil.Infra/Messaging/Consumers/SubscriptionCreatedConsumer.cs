using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Subscriptions.Email;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers
{
    public class SubscriptionCreatedConsumer(
        IEmailSender emailSender,
        ILogger<SubscriptionCreatedConsumer> logger
    ) : IConsumer<SubscriptionCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<SubscriptionCreatedIntegrationEvent> context)
        {
            var msg = context.Message;

            var subject = $"Confirmação de assinatura";

            var body = SubscriptionEmailBuilder.BuildHtml(msg.ClientName, msg.PlanName, msg.PlanPrice, msg.PaymentLink);

            try
            {
                await emailSender.SendEmailAsync(
                    toEmail: msg.ClientEmail,
                    subject: subject,
                    htmlBody: body
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

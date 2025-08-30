using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Payments.Email;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers
{
    public class PaymentApprovedConsumer(ILogger<PaymentApprovedConsumer> logger, IEmailSender emailSender) : IConsumer<PaymentApprovedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<PaymentApprovedIntegrationEvent> context)
        {
            var msg = context.Message;

            var subject = $"Seu pagamento foi aprovado";

            var body = PaymentEmailBuilder.BuildHtml(msg.ClientName, msg.Amount, msg.PaidDate, msg.PlanName);

            try
            {
                await emailSender.SendEmailAsync(
                    toEmail: msg.ClientEmail,
                    subject: subject,
                    htmlBody: body
                );

                logger.LogInformation("mail de pagamento aprovado enviado para {Email}\"", msg.ClientEmail);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao enviar email para {Email}", msg.ClientEmail);
                throw;
            }
        }
    }
}

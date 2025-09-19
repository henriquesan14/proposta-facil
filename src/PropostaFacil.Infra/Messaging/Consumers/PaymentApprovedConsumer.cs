using MassTransit;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers
{
    public class PaymentApprovedConsumer(ILogger<PaymentApprovedConsumer> logger, IEmailService emailService) : IConsumer<PaymentApprovedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<PaymentApprovedIntegrationEvent> context)
        {
            var msg = context.Message;
            try
            {
                await emailService.SendConfirmPayment(
                    msg.ClientEmail, msg.ClientName, msg.Amount, msg.PaidDate, msg.PlanName
                );

                logger.LogInformation("Email de pagamento aprovado enviado para {Email}\"", msg.ClientEmail);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao enviar email para {Email}", msg.ClientEmail);
                throw;
            }
        }
    }
}

using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers;

public class ForgotPasswordConsumer(
    IEmailService emailService,
    ILogger<ForgotPasswordConsumer> logger,
    IConfiguration configuration
) : IConsumer<ForgotPasswordIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ForgotPasswordIntegrationEvent> context)
    {
        var msg = context.Message;
        var resetPasswordLink = $"{configuration["AppUrl"]}/reset-password?userId={msg.UserId}&token={msg.ForgotToken}";

        try
        {
            await emailService.SendForgotPassword(msg.Email, msg.Name, resetPasswordLink);

            logger.LogInformation("Email de esqueceu a senha {Email}\"", msg.Email);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao enviar email para {Email}", msg.Email);
            throw;
        }
    }
}

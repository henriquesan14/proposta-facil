using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Messaging.Consumers;

public class UserCreatedConsumer(ILogger<UserCreatedConsumer> logger, IEmailService emailService, IConfiguration configuration) : IConsumer<UserCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
    {
        var msg = context.Message;
        var activationLink = $"{configuration["AppUrl"]}/activate-account?token={msg.verifiedToken}";
        try
        {
            await emailService.SendVerifyEmailAddress(
                msg.email,
                msg.name,
                activationLink
            );

            logger.LogInformation("Email de verificação para {Email}\"", msg.email);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao enviar email para {Email}", msg.email);
            throw;
        }
    }
}

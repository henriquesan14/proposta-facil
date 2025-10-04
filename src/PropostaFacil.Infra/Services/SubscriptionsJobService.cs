using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Payments.Specifications;
using PropostaFacil.Domain.Subscriptions.Specifications;

namespace PropostaFacil.Infra.Services;

public class SubscriptionsJobService(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    ILogger<SubscriptionsJobService> logger,
    IConfiguration configuration
) : ISubscriptionsJobService
{
    public async Task CheckOverduePayments()
    {
        logger.LogInformation("⏰ Iniciando job de expiração de assinaturas vencidas em {Date}", DateTime.Now);

        var overdueSubscriptions = await unitOfWork.Subscriptions
            .ListAsync(new GetOverdueSubscriptionsSpecification());

        if (!overdueSubscriptions.Any())
        {
            logger.LogInformation("Nenhuma assinatura vencida encontrada.");
            return;
        }

        foreach (var subscription in overdueSubscriptions)
        {
            var lastPayment = await unitOfWork.Payments
                .SingleOrDefaultAsync(new GetLastPaymentBySubscriptionIdSpecification(subscription.Id));

            if (lastPayment?.DueDate == null)
                continue;

            var dueDate = lastPayment.DueDate.ToDateTime(TimeOnly.MinValue);
            var daysOverdue = (DateTime.Now.Date - dueDate.Date).Days;

            if (int.TryParse(configuration["DaysBeforeExpireSubscription"], out var daysBeforeExpire))
            {
                if (daysOverdue < daysBeforeExpire)
                    continue;
            }
            else
            {
                daysBeforeExpire = 3;
            }

            subscription.Expire();

            logger.LogInformation("Expirando assinatura {SubscriptionId} (vencida há {Days} dias)", subscription.Id, daysOverdue);

            try
            {
                await emailService.SendSubscriptionExpired(
                    subscription.Tenant.Contact.Email,
                    subscription.Tenant.Name,
                    lastPayment.PaymentLink,
                    lastPayment.Amount,
                    lastPayment.DueDate
                );

                logger.LogInformation("E-mail de expiração enviado para {Email}", subscription.Tenant.Contact.Email);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao enviar e-mail de expiração para {Email}", subscription.Tenant.Contact.Email);
            }
        }

        await unitOfWork.CompleteAsync();

        logger.LogInformation("✅ Job de expiração concluído com sucesso ({Count} assinaturas processadas)", overdueSubscriptions.Count);
    }
}

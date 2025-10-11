using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Shared.Request;
using PropostaFacil.Domain.Payments.Specifications;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using System.Globalization;

namespace PropostaFacil.Application.Subscriptions.Commands.ChangeSubscriptionPlan;

public class ChangeSubscriptionPlanCommandHandler(IUnitOfWork unitOfWork, IAsaasService asaasService) : ICommandHandler<ChangeSubscriptionPlanCommand, Result>
{
    public async Task<Result> Handle(ChangeSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscription = await unitOfWork.Subscriptions.SingleOrDefaultAsync(new GetSubscriptionAccountSpecification());
        if (subscription == null) return SubscriptionErrors.NoSubscriptionTenant();

        var newPlan = await unitOfWork.SubscriptionPlans.GetByIdAsync(SubscriptionPlanId.Of(request.SubscriptionPlanId));
        if (newPlan is null)
            return SubscriptionErrors.NotFound(request.SubscriptionPlanId);

        var isUpgrade = newPlan.Price > subscription.SubscriptionPlan.Price;

        var lastPayment = await unitOfWork.Payments
                .SingleOrDefaultAsync(new GetLastPaymentBySubscriptionIdSpecification(subscription.Id));

        var lastPaymentDate = lastPayment != null
                ? lastPayment.DueDate.ToDateTime(TimeOnly.MinValue)
                : subscription.CreatedAt!.Value.Date;

        var nextDueDate = lastPaymentDate.AddMonths(1);

        if (isUpgrade)
        {
            var difference = CalculateProRataDifference(
                subscription.SubscriptionPlan.Price,
                newPlan.Price,
                nextDueDate,
                lastPaymentDate
            );

            // gera cobrança avulsa no Asaas
            if (difference > 0 && !string.IsNullOrEmpty(subscription.Tenant.AsaasId))
            {
                var paymentRequest = new PaymentAsaasRequest
                (
                    subscription.Tenant.AsaasId,
                    difference,
                    request.BillingType.ToString(),
                    nextDueDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    $"Upgrade para {newPlan.Name} (diferença proporcional)",
                    subscription.Id.Value.ToString()
                );
                var response = await asaasService.CreatePayment(paymentRequest);

                subscription.AddPayment(difference, DateOnly.FromDateTime(nextDueDate), request.BillingType, response.Id, response.InvoiceUrl);
            }

            // atualiza assinatura com novo valor
            if (!string.IsNullOrEmpty(subscription.SubscriptionAsaasId))
            {
                var updateRequest = new UpdateSubscriptionRequest(
                    request.BillingType,
                    newPlan.Price,
                    nextDueDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    newPlan.Description
                );

                await asaasService.UpdateSubscription(subscription.SubscriptionAsaasId, updateRequest);
            }

            // atualiza no sistema
            subscription.ChangePlan(newPlan.Id);
            await unitOfWork.CompleteAsync();

            return Result.Success();
        }

        if (!isUpgrade)
        {
            if (!string.IsNullOrEmpty(subscription.SubscriptionAsaasId))
            {
                var updateRequest = new UpdateSubscriptionRequest(
                    request.BillingType,
                    newPlan.Price,
                    nextDueDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    newPlan.Description,
                    updatePendingPayments: false
                );

                await asaasService.UpdateSubscription(subscription.SubscriptionAsaasId, updateRequest);
            }

            subscription.ChangePlan(newPlan.Id);
            await unitOfWork.CompleteAsync();

            return Result.Success();
        }

        return Result.Success();
    }

    private decimal CalculateProRataDifference(
        decimal currentPrice,
        decimal newPrice,
        DateTime currentDueDate,
        DateTime lastPaymentDate)
    {
        var totalDays = (currentDueDate - lastPaymentDate).TotalDays;
        var remainingDays = (currentDueDate - DateTime.Now).TotalDays;
        if (totalDays <= 0 || remainingDays <= 0) return 0;

        var currentProRata = currentPrice * (decimal)(remainingDays / totalDays);
        var newProRata = newPrice * (decimal)(remainingDays / totalDays);

        var difference = newProRata - currentProRata;
        return difference < 0 ? 0 : decimal.Round(difference, 2);
    }
}

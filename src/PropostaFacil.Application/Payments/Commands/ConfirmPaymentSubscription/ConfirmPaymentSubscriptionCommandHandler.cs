using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Payments.Specifications;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Domain.Tenants.Specifications;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Payments.Commands.ConfirmPaymentSubscription;

public record ConfirmPaymentSubscriptionCommandHandler(IUnitOfWork unitOfWork, IPasswordHash passwordHash, IUserRuleCheck userRuleCheck) : ICommandHandler<ConfirmPaymentSubscriptionCommand, Result>
{
    public async Task<Result> Handle(ConfirmPaymentSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var paymentExists = await unitOfWork.Payments.FirstOrDefaultAsync(new GetPaymentByAsaasIdSpecification(request.Payment.Id));
        if(paymentExists != null) return PaymentErrors.PaymentAlreadyExist(request.Payment.Id);

        var spec = new GetSubscriptionByAsaasIdSpecification(request.Payment.Subscription);
        var subscription = await unitOfWork.Subscriptions.SingleOrDefaultAsync(spec);

        if (subscription is null) return PaymentErrors.NotFound(request.Payment.Subscription); 
        if (request.Event != "PAYMENT_RECEIVED")
            return PaymentErrors.InvalidEvent();
        if (subscription.Status == Domain.Enums.SubscriptionStatusEnum.Active)
            return PaymentErrors.SubscriptionAlreadyActive();

        var countPayments = await unitOfWork.Payments.CountAsync(new GetPaymentsBySubscriptionIdSpecification(subscription.Id));
        
        subscription.Activate();
        subscription.ResetProposalsUsed();

        if (countPayments == 0) // Primeiro pagamento
        {
            subscription.AddPayment(
                request.Payment.Value,
                request.Payment.PaymentDate,
                request.Payment.BillingType,
                request.Payment.Id,
                request.Payment.InvoiceUrl,
                true
            );

            var tenant = await unitOfWork.Tenants.SingleOrDefaultAsync(new GetTenantByIdGlobalSpecification(subscription.TenantId));
            var hasUsers = await unitOfWork.Users.AnyAsync(new GetUsersByTenantIdGlobalSpecification(tenant!.Id));

            if (!hasUsers)
            {
                var adminUser = User.CreateWithoutPassword(
                    tenant.Name,
                    tenant.Contact,
                    UserRoleEnum.AdminTenant,
                    tenant.Id,
                    passwordHash,
                    userRuleCheck
                );

                await unitOfWork.Users.AddAsync(adminUser);
            }
        }
        else
        {
            subscription.AddPayment(
                request.Payment.Value,
                request.Payment.PaymentDate,
                request.Payment.BillingType,
                request.Payment.Id,
                request.Payment.InvoiceUrl
            );
        }

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}

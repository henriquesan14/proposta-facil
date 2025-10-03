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

namespace PropostaFacil.Application.Payments.Commands.PaymentReceived;

public record PaymentReceivedCommandHandler(IUnitOfWork unitOfWork, IPasswordHash passwordHash, IUserRuleCheck userRuleCheck) : ICommandHandler<PaymentReceivedCommand, Result>
{
    public async Task<Result> Handle(PaymentReceivedCommand request, CancellationToken cancellationToken)
    {
        if (request.Event != "PAYMENT_RECEIVED")
            return PaymentErrors.InvalidEvent();
        var payload = request.Payment;
        var payment = await unitOfWork.Payments.SingleOrDefaultAsync(new GetPaymentByAsaasIdSpecification(payload.Id));
        if (payment is null) return PaymentErrors.NotFound(payload.Id);

        if (payment.Status == PaymentStatus.RECEIVED)
        {
            return Result.Success();
        }

        var subscription = await unitOfWork.Subscriptions.SingleOrDefaultAsync(new GetSubscriptionByAsaasIdSpecification(payload.Subscription));

        if (subscription is null) return PaymentErrors.NotFound(payload.Subscription); 
        
        if (subscription.Status == Domain.Enums.SubscriptionStatusEnum.Active)
            return PaymentErrors.SubscriptionAlreadyActive();

        await unitOfWork.BeginTransaction();

        payment.ConfirmSubscriptionPayment(payload.PaymentDate!.Value);

        if(subscription.Status == SubscriptionStatusEnum.Pending)
        {
            subscription.Activate();
            subscription.ResetProposalsUsed();

            var tenant = await unitOfWork.Tenants.SingleOrDefaultAsync(new GetTenantByIdGlobalSpecification(subscription.TenantId));
            var hasUsers = await unitOfWork.Users.AnyAsync(new GetUsersByTenantIdGlobalSpecification(tenant!.Id));

            if (!hasUsers)
            {
                var adminUser = User.Create(
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
        else if (subscription.Status is SubscriptionStatusEnum.Expired
        or SubscriptionStatusEnum.Canceled
        or SubscriptionStatusEnum.Suspended)
        {
            subscription.Reactivate();
            subscription.ResetProposalsUsed();
        }

        await unitOfWork.CompleteAsync();
        await unitOfWork.CommitAsync();

        return Result.Success();
    }
}

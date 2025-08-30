using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Shared.Common.CQRS;
using System.Linq.Expressions;

namespace PropostaFacil.Application.Payments.Commands.ConfirmPaymentSubscription
{
    public record ConfirmPaymentSubscriptionCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<ConfirmPaymentSubscriptionCommand, Result>
    {
        public async Task<Result> Handle(ConfirmPaymentSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var paymentExists = await unitOfWork.Payments.GetSingleAsync(p => p.PaymentAsaasId == request.Payment.Id);
            if(paymentExists != null) return PaymentErrors.PaymentAlreadyExist(request.Payment.Id);

            List<Expression<Func<Subscription, object>>> includesSubscription = new List<Expression<Func<Subscription, object>>>()
            {
                s => s.SubscriptionPlan,
                s => s.Tenant
            };

            var subscription = await unitOfWork.Subscriptions.GetSingleAsync(s => s.SubscriptionAsaasId == request.Payment.Subscription, includes: includesSubscription);

            if (subscription is null) return PaymentErrors.NotFound(request.Payment.Subscription); 
            if (request.Event != "PAYMENT_RECEIVED")
                return PaymentErrors.InvalidEvent();
            if (subscription.Status == Domain.Enums.SubscriptionStatusEnum.Active)
                return PaymentErrors.SubscriptionAlreadyActive();
            
            subscription.Activate();
            subscription.AddPayment(request.Payment.Value, request.Payment.PaymentDate, request.Payment.BillingType, request.Payment.Id, request.Payment.InvoiceUrl);

            await unitOfWork.CompleteAsync();

            return Result.Success();
        }
    }
}

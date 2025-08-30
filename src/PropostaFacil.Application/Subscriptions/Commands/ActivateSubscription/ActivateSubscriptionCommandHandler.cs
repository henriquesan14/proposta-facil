using Common.ResultPattern;
using PropostaFacil.Application.Payments;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Commands.ActivateSubscription
{
    public record ActivateSubscriptionCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<ActivateSubscriptionCommand, Result>
    {
        public async Task<Result> Handle(ActivateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var paymentExists = await unitOfWork.Payments.GetSingleAsync(p => p.PaymentAsaasId == request.Payment.Id);
            if(paymentExists != null) return PaymentErrors.PaymentAlreadyExist(request.Payment.Id);
            var subscription = await unitOfWork.Subscriptions.GetSingleAsync(s => s.SubscriptionAsaasId == request.Payment.Subscription);
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

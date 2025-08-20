using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateSubscriptionCommand, ResultT<SubscriptionResponse>>
    {
        public async Task<ResultT<SubscriptionResponse>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var hasSubscriptionActive = await unitOfWork.Subscriptions.GetSingleAsync(s => s.TenantId == TenantId.Of(request.TenantId) && s.Status == Domain.Enums.SubscriptionStatusEnum.Active);
            if (hasSubscriptionActive != null) return SubscriptionErrors.Conflict();

            var subscription = Subscription.Create(TenantId.Of(request.TenantId), SubscriptionPlanId.Of(request.SubscriptionPlanId), request.StartDate, request.EndDate);

            await unitOfWork.Subscriptions.AddAsync(subscription);
            await unitOfWork.CompleteAsync();

            return subscription.ToDto();
        }
    }
}

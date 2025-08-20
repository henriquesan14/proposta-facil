using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Commands.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateSubscriptionPlanCommand, ResultT<SubscriptionPlanResponse>>
    {
        public async Task<ResultT<SubscriptionPlanResponse>> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
        {
            var subscriptionPlanExist = await unitOfWork.SubscriptionPlans.GetSingleAsync(sp => sp.Name.ToLower().Equals(request.Name.ToLower()));
            if (subscriptionPlanExist != null) return SubscriptionPlanErrors.Conflict(request.Name);

            var subscriptionPlan = SubscriptionPlan.Create(request.Name, request.MaxProposalsPerMonth, request.Price, request.Description);

            await unitOfWork.SubscriptionPlans.AddAsync(subscriptionPlan);

            await unitOfWork.CompleteAsync();

            return subscriptionPlan.ToDto();
        }
    }
}

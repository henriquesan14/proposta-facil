using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.SubscriptionPlans;
using PropostaFacil.Domain.SubscriptionPlans.Contracts;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.SubscriptionPlans.Commands.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanCommandHandler(IUnitOfWork unitOfWork, ICacheService memoryCacheService, ISubscriptionPlanRuleCheck subscriptionPlanRuleCheck) : ICommandHandler<CreateSubscriptionPlanCommand, ResultT<SubscriptionPlanResponse>>
    {
        public async Task<ResultT<SubscriptionPlanResponse>> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
        {
            var subscriptionPlan = SubscriptionPlan.Create(request.Name, request.MaxProposalsPerMonth, request.Price, request.Description, subscriptionPlanRuleCheck);

            await unitOfWork.SubscriptionPlans.AddAsync(subscriptionPlan);

            await unitOfWork.CompleteAsync();

            await memoryCacheService.RemoveByPrefix("SubscriptionPlans:*");

            return subscriptionPlan.ToDto();
        }
    }
}

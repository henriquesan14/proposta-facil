using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.SubscriptionPlans.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.SubscriptionPlans.Commands.DeleteSubscriptionPlan;

public class DeleteSubscriptionPlanCommandHandler(IUnitOfWork unitOfWork, ICacheService memoryCacheService) : ICommandHandler<DeleteSubscriptionPlanCommand, Result>
{
    public async Task<Result> Handle(DeleteSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = await unitOfWork.SubscriptionPlans.SingleOrDefaultAsync(new GetSubscriptionPlanByIdGlobalSpecification(SubscriptionPlanId.Of(request.Id)));
        if (subscriptionPlan is null) return SubscriptionPlanErrors.NotFound(request.Id);

        unitOfWork.SubscriptionPlans.Remove(subscriptionPlan);

        await unitOfWork.CompleteAsync();

        await memoryCacheService.RemoveByPrefix("SubscriptionPlans:*");

        return Result.Success();
    }
}

using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler(IUnitOfWork unitOfWork, IAsaasService asaasService) : ICommandHandler<DeleteSubscriptionCommand, Result>
{
    public async Task<Result> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await unitOfWork.Subscriptions.SingleOrDefaultAsync(new GetSubscriptionByIdGlobalSpecification(SubscriptionId.Of(request.Id)));
        if (subscription == null) return SubscriptionErrors.NotFound(request.Id);

        await unitOfWork.BeginTransaction();

        unitOfWork.Subscriptions.SoftDelete(subscription);

        var response = await asaasService.DeleteSubscription(subscription.SubscriptionAsaasId);

        await unitOfWork.Payments.SoftDeleteBySubscriptionId(subscription.Id);

        await unitOfWork.CompleteAsync();
        await unitOfWork.CommitAsync();

        return Result.Success();
    }
}

using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionById;

public class GetSubscriptionByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubscriptionByIdQuery, ResultT<SubscriptionResponse>>
{
    public async Task<ResultT<SubscriptionResponse>> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var subscription = await unitOfWork.Subscriptions.SingleOrDefaultAsync(new GetSubscriptionByIdGlobalSpecification(SubscriptionId.Of(request.Id)));
        if (subscription == null) return SubscriptionErrors.NotFound(request.Id);

        return subscription.ToDto();
    }
}

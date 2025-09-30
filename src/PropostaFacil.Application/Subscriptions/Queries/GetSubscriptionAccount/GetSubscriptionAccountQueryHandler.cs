using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionAccount;

public class GetSubscriptionAccountQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubscriptionAccountQuery, ResultT<SubscriptionAccountResponse>>
{
    public async Task<ResultT<SubscriptionAccountResponse>> Handle(GetSubscriptionAccountQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await unitOfWork.Subscriptions.ListAsync(new GetSubscriptionsByTenantSpecification());

        if (subscriptions is null || !subscriptions.Any()) return SubscriptionErrors.NoSubscriptionTenant();

        var active = subscriptions.FirstOrDefault(s => s.Status == SubscriptionStatusEnum.Active);

        var response = new SubscriptionAccountResponse
        (
            active is null ? null : active.ToDto(),
            subscriptions
                .Where(s => s.Status != SubscriptionStatusEnum.Active)
                .ToDto()
        );

        return response;
    }
}

using Common.ResultPattern;
using PropostaFacil.Application.Payments;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Payments.Specifications;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionAccount;

public class GetSubscriptionAccountQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubscriptionAccountQuery, ResultT<SubscriptionAccountResponse>>
{
    public async Task<ResultT<SubscriptionAccountResponse>> Handle(GetSubscriptionAccountQuery request, CancellationToken cancellationToken)
    {
        var subscription = await unitOfWork.Subscriptions
            .SingleOrDefaultAsync(new GetSubscriptionAccountSpecification());

        if (subscription is null)
            return SubscriptionErrors.NoSubscriptionTenant();

        var spec = new ListPaymentsBySubscriptionIdSpecification(subscription.Id);
        var payments = await unitOfWork.Payments
            .ToPaginatedListAsync(spec, request.PageIndex, request.PageSize, u => u.ToDto());

        var response = new SubscriptionAccountResponse
        (
            subscription.ToDto(),
            payments
        );

        return response;
    }
}

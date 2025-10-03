using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Payments.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Payments.Queries.GetPaymentsBySubscription;

public class GetPaymentsBySubscriptionQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetPaymentsBySubscriptionQuery, ResultT<PaginatedResult<PaymentResponse>>>
{
    public async Task<ResultT<PaginatedResult<PaymentResponse>>> Handle(GetPaymentsBySubscriptionQuery request, CancellationToken cancellationToken)
    {
        var spec = new ListPaymentsBySubscriptionIdSpecification(SubscriptionId.Of(request.SubscriptionId));
        var payments = await unitOfWork.Payments
            .ToPaginatedListAsync(spec, request.PageIndex, request.PageSize, p => p.ToDto());

        return payments;
    }
}

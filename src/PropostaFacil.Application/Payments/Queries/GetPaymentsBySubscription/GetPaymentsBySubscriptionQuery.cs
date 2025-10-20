using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Payments.Queries.GetPaymentsBySubscription;

public record GetPaymentsBySubscriptionQuery(Guid SubscriptionId, int PageIndex = 1, int PageSize = 10) : IQuery<ResultT<PaginatedResult<PaymentResponse>>>;


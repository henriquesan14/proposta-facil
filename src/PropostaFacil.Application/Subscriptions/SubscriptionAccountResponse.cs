using PropostaFacil.Application.Payments;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Subscriptions;

public record SubscriptionAccountResponse(SubscriptionResponse? ActiveSubscription, PaginatedResult<PaymentResponse> Payments);
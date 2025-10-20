using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionAccount;

public record GetSubscriptionAccountQuery(int PageIndex = 1, int PageSize = 10) : IQuery<ResultT<SubscriptionAccountResponse>>;

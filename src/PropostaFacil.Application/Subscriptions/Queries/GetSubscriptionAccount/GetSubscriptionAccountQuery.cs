using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionAccount;

public record GetSubscriptionAccountQuery : IQuery<ResultT<SubscriptionAccountResponse>>;

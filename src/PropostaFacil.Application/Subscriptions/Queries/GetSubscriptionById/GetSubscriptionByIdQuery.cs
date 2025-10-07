using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionById;

public record GetSubscriptionByIdQuery(Guid Id) : IQuery<ResultT<SubscriptionResponse>>;

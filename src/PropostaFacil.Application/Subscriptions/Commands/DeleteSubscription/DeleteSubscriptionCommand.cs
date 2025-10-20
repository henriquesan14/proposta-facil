using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Commands.DeleteSubscription;

public record DeleteSubscriptionCommand(Guid Id) : ICommand<Result>;

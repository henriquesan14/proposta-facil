using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.SubscriptionPlans.Commands.DeleteSubscriptionPlan;

public record DeleteSubscriptionPlanCommand(Guid Id) : ICommand<Result>;

using Common.ResultPattern;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Commands.ChangeSubscriptionPlan;

public record ChangeSubscriptionPlanCommand(Guid SubscriptionPlanId, BillingTypeEnum BillingType) : ICommand<Result>;

using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.SubscriptionPlans.Commands.CreateSubscriptionPlan
{
    public record CreateSubscriptionPlanCommand(string Name, int MaxProposalsPerMonth, decimal Price, string Description) : ICommand<ResultT<SubscriptionPlanResponse>>;
}

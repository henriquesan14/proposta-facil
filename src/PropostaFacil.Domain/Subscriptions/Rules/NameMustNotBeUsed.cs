using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Subscriptions.Contracts;

namespace PropostaFacil.Domain.Subscriptions.Rules;

public class NameMustNotBeUsed(string subscriptionPlanName, ISubscriptionPlanRuleCheck check) : IBusinessRule
{
    public string Message => "SubscriptionPlan name is used";

    public bool IsBroken()
    {
        return check.SubscriptionPlanNameExists(subscriptionPlanName);
    }
}

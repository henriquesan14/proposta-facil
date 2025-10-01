using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.SubscriptionPlans.Contracts;

namespace PropostaFacil.Domain.SubscriptionPlans.Rules;

public class NameMustNotBeUsed(string subscriptionPlanName, ISubscriptionPlanRuleCheck check) : IBusinessRule
{
    public string Message => "SubscriptionPlan name is used";

    public bool IsBroken()
    {
        return check.SubscriptionPlanNameExists(subscriptionPlanName);
    }
}

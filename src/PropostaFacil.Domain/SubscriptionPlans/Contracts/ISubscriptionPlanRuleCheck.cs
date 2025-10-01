namespace PropostaFacil.Domain.SubscriptionPlans.Contracts;

public interface ISubscriptionPlanRuleCheck
{
    bool SubscriptionPlanNameExists(string  subscriptionPlanName);
}

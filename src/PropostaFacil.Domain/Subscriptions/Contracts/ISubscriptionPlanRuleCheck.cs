namespace PropostaFacil.Domain.Subscriptions.Contracts;

public interface ISubscriptionPlanRuleCheck
{
    bool SubscriptionPlanNameExists(string  subscriptionPlanName);
}

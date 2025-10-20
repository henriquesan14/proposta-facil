using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.SubscriptionPlans.Contracts;
using PropostaFacil.Domain.SubscriptionPlans.Rules;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.SubscriptionPlans;

public class SubscriptionPlan : Aggregate<SubscriptionPlanId>
{
    public static SubscriptionPlan Create(string name, int maxProposalsPerMonth, decimal price, string description, ISubscriptionPlanRuleCheck subscriptionPlanRuleCheck)
    {
        CheckRule(new NameMustNotBeUsed(name, subscriptionPlanRuleCheck));
        return new SubscriptionPlan
        {
            Id = SubscriptionPlanId.Of(Guid.NewGuid()),
            Name = name,
            MaxProposalsPerMonth = maxProposalsPerMonth,
            Price = price,
            Description = description
        };
    }

    public string Name { get; private set; } = default!;
    public int MaxProposalsPerMonth { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; } = default!;

    public ICollection<Subscription> Subscriptions { get; private set; } = default!;

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}

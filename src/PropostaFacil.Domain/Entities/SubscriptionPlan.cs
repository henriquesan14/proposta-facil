using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Entities
{
    public class SubscriptionPlan : Aggregate<SubscriptionPlanId>
    {
        public static SubscriptionPlan Create(string name, int maxProposalsPerMonth, decimal price, string description)
        {
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
}

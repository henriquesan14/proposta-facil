using PropostaFacil.Domain.Exceptions;

namespace PropostaFacil.Domain.ValueObjects.Ids;

public record SubscriptionPlanId
{
    public Guid Value { get; }
    private SubscriptionPlanId(Guid value) => Value = value;
    public static SubscriptionPlanId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("SubscriptionPlanId cannot be empty.");
        }
        return new SubscriptionPlanId(value);
    }
}

using PropostaFacil.Domain.Exceptions;

namespace PropostaFacil.Domain.ValueObjects.Ids;

public record SubscriptionId
{
    public Guid Value { get; }
    private SubscriptionId(Guid value) => Value = value;
    public static SubscriptionId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("SubscriptionId cannot be empty.");
        }
        return new SubscriptionId(value);
    }
}

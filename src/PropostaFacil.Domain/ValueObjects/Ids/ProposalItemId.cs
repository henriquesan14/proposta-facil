using PropostaFacil.Domain.Exceptions;

namespace PropostaFacil.Domain.ValueObjects.Ids;

public record ProposalItemId
{
    public Guid Value { get; }
    private ProposalItemId(Guid value) => Value = value;
    public static ProposalItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("ProposalItemId cannot be empty.");
        }
        return new ProposalItemId(value);
    }
}

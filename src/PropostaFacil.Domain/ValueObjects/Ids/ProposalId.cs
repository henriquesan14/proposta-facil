using PropostaFacil.Domain.Exceptions;

namespace PropostaFacil.Domain.ValueObjects.Ids
{
    public record ProposalId
    {
        public Guid Value { get; }

        private ProposalId(Guid value) => Value = value;

        public static ProposalId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("ProposalId cannot be empty.");
            }
            return new ProposalId(value);
        }
    }
}

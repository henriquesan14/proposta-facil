using PropostaFacil.Domain.Exceptions;

namespace PropostaFacil.Domain.ValueObjects.Ids
{
    public record PaymentId
    {
        public Guid Value { get; }

        private PaymentId(Guid value) => Value = value;

        public static PaymentId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("PaymentId cannot be empty.");
            }
            return new PaymentId(value);
        }
    }
}

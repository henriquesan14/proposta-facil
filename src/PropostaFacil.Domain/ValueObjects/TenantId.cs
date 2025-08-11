using PropostaFacil.Domain.Exceptions;

namespace PropostaFacil.Domain.ValueObjects
{
    public record TenantId
    {
        public Guid Value { get; }
        private TenantId(Guid value) => Value = value;
        public static TenantId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("CompanyId cannot be empty.");
            }
            return new TenantId(value);
        }
    }
}

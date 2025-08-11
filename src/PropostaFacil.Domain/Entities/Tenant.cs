using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.ValueObjects;

namespace PropostaFacil.Domain.Entities
{
    public class Tenant : Aggregate<TenantId>
    {
        public static Tenant Create(string name, string cnpj, string domain)
        {
            return new Tenant
            {
                Id = TenantId.Of(Guid.NewGuid()),
                Name = name,
                Cnpj = cnpj,
                Domain = domain
            };
        }
        public string Name { get; private set; } = default!;
        public string Cnpj { get; private set; } = default!;
        public string Domain { get; private set; } = default!;

    }
}

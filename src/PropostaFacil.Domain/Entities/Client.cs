using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Entities
{
    public class Client : Aggregate<ClientId>
    {
        public string Name { get; private set; } = default!;

        public Document Document { get; private set; } = default!;
        public Contact Contact { get; private set; } = default!;

        public Address Address { get; private set; } = default!;

        public TenantId TenantId { get; private set; } = default!;

        public Tenant Tenant { get; private set; } = default!;
        public ICollection<Proposal> Proposals { get; private set; } = default!;
    }
}

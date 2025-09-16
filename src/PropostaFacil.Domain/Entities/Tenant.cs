using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Clients;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Entities
{
    public class Tenant : Aggregate<TenantId>
    {
        public static Tenant Create(string name, string domain, Document document, Contact contact, Address address, string asaasId)
        {
            return new Tenant
            {
                Id = TenantId.Of(Guid.NewGuid()),
                Name = name,
                Domain = domain,
                Document = document,
                Contact = contact,
                Address = address,
                AsaasId = asaasId
            };
        }

        public void Update(string name, string domain, Document document,  Contact contact, Address address)
        {
            Name = name;
            Domain = domain;
            Document = document;
            Contact = contact;
            Address = address;
        }
        public string Name { get; private set; } = default!;
        public string Domain { get; private set; } = default!;
        public string AsaasId { get; private set; } = default!;
        public Document Document { get; private set; } = default!;

        public Contact Contact { get; private set; } = default!;

        public Address Address { get; private set; } = default!;

        public ICollection<Client> Clients { get; private set; } = default!;

        public ICollection<User> Users { get; private set; } = default!;

        public ICollection<Subscription> Subscriptions { get; private set; } = default!;

    }
}

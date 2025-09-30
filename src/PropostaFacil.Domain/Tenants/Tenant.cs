using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Clients;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.Tenants.Contracts;
using PropostaFacil.Domain.Tenants.Rules;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Tenants
{
    public class Tenant : Aggregate<TenantId>
    {
        public static Tenant Create(string name, string domain, Document document, Contact contact, Address address, string asaasId, ITenantRuleCheck tenantRuleCheck)
        {
            CheckRule(new DocumentMustNotBeUsed(document.Number, tenantRuleCheck));
            CheckRule(new EmailMustNotBeUsed(contact.Email, tenantRuleCheck));
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

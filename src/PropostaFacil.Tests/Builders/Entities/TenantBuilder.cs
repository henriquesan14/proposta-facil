using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects;

namespace PropostaFacil.Tests.Builders.Entities
{
    public class TenantBuilder
    {
        private string _name = "Default Tenant";
        private string _domain = "default.com";
        private Document _document = Document.Of("12345678000199");
        private Contact _contact = Contact.Of("tenant@example.com", "11999999999");
        private Address _address = Address.Of("Street", "123", "Apt 1", "District", "City", "ST", "12345000");

        public TenantBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public TenantBuilder WithDomain(string domain)
        {
            _domain = domain;
            return this;
        }

        public TenantBuilder WithDocument(Document document)
        {
            _document = document;
            return this;
        }

        public TenantBuilder WithContact(Contact contact)
        {
            _contact = contact;
            return this;
        }

        public TenantBuilder WithAddress(Address address)
        {
            _address = address;
            return this;
        }

        public Tenant Build()
        {
            return Tenant.Create(_name, _domain, _document, _contact, _address);
        }
    }
}

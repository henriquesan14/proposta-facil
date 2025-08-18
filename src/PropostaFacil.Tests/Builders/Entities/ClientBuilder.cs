using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Tests.Builders.Entities
{
    public class ClientBuilder
    {
        private string _name = "Cliente Teste";
        private TenantId _tenantId = TenantId.Of(Guid.NewGuid());
        private Document _document = Document.Of("12345678900");
        private Contact _contact = Contact.Of("cliente@email.com", "+5511999999999");
        private Address _address = Address.Of("Rua Teste", "123", "","São Paulo", "SP", "Brasil", "01000-000");

        public Client Build()
            => Client.Create(_name, _tenantId, _document, _contact, _address);

        public ClientBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ClientBuilder WithTenantId(TenantId tenantId)
        {
            _tenantId = tenantId;
            return this;
        }

        public ClientBuilder WithDocument(Document document)
        {
            _document = document;
            return this;
        }

        public ClientBuilder WithContact(Contact contact)
        {
            _contact = contact;
            return this;
        }

        public ClientBuilder WithAddress(Address address)
        {
            _address = address;
            return this;
        }
    }
}

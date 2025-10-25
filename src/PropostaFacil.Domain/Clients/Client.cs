using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Clients.Contracts;
using PropostaFacil.Domain.Clients.Rules;
using PropostaFacil.Domain.Proposals;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Clients;

public class Client : Aggregate<ClientId>
{
    public static Client Create(string name, TenantId? tenantId, Document document, Contact contact, Address address, IClientRuleCheck clientDocumentCheck)
    {
        CheckRule(new DocumentMustNotBeUsed(document, clientDocumentCheck));
        CheckRule(new EmailMustNotBeUsed(contact.Email, clientDocumentCheck));
        return new Client
        {
            Id = ClientId.Of(Guid.NewGuid()),
            Name = name,
            TenantId = tenantId!,
            Document = document,
            Contact = contact,
            Address = address
        };
    }

    public void Update(string name, Document document, Contact contact, Address address)
    {
        Name = name;
        Document = document;
        Contact = contact;
        Address = address;
    }
    public string Name { get; private set; } = default!;

    public Document Document { get; private set; } = default!;
    public Contact Contact { get; private set; } = default!;

    public Address Address { get; private set; } = default!;

    public TenantId TenantId { get; private set; } = default!;

    public Tenant Tenant { get; private set; } = default!;
    public ICollection<Proposal> Proposals { get; private set; } = default!;
}

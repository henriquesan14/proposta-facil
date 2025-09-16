using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Clients.Contracts;
using PropostaFacil.Domain.ValueObjects;

namespace PropostaFacil.Domain.Clients.Rules;

internal class DocumentMustNotBeUsed(Document document, IClientRuleCheck check) : IBusinessRule
{
    public string Message => "Document client is used";

    public bool IsBroken()
    {
        return check.DocumentExists(document);
    }
}

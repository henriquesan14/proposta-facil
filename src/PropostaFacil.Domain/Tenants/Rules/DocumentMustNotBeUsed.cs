using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Tenants.Contracts;

namespace PropostaFacil.Domain.Tenants.Rules;

internal class DocumentMustNotBeUsed(string document, ITenantRuleCheck check) : IBusinessRule
{
    public string Message => "Document tenant is used";

    public bool IsBroken()
    {
        return check.TenantDocumentExists(document);
    }
}

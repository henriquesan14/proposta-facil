using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Tenants.Contracts;

namespace PropostaFacil.Domain.Tenants.Rules;

internal class EmailMustNotBeUsed(string email, ITenantRuleCheck check) : IBusinessRule
{
    public string Message => "Email tenant is used";

    public bool IsBroken()
    {
        return check.TenantEmailExists(email);
    }
}

using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Clients.Contracts;

namespace PropostaFacil.Domain.Clients.Rules;

internal class EmailMustNotBeUsed(string email, IClientRuleCheck check) : IBusinessRule
{
    public string Message => "Email client is used";

    public bool IsBroken()
    {
        return check.EmailExists(email);
    }
}

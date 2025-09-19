using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Users.Contracts;

namespace PropostaFacil.Domain.Users.Rules;

internal class EmailMustNotBeUsed(string email, IUserRuleCheck check) : IBusinessRule
{
    public string Message => "Email user is used";

    public bool IsBroken()
    {
        return check.UserEmailExists(email);
    }
}

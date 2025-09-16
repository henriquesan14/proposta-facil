using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Users.Contracts;

namespace PropostaFacil.Domain.Users.Rules;

internal class PasswordMustMatchRule(User user, string password, IPasswordCheck check) : IBusinessRule
{
    public string Message => "The password does not match";

    public bool IsBroken()
    {
        return check.Matches(password, user.PasswordHash) == false;
    }
}
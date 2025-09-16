using PropostaFacil.Domain.Abstractions;

namespace PropostaFacil.Domain.Users.Rules;
internal class ForgotTokenMustNotBeExpiredRule(User user) : IBusinessRule
{
    public string Message => "The forgot token has expired";

    public bool IsBroken()
    {
        return DateTime.Now > user.ForgottenTokenExpiry;
    }
}
using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Users.Rules;

internal class MustNotBeSelfRule(User user, IUserContext context) : IBusinessRule
{
    public string Message => "This action can only be performed on others";

    public bool IsBroken()
    {
        var userId = UserId.Of(context.UserId!.Value);
        return user.Id.Equals(userId);
    }
}
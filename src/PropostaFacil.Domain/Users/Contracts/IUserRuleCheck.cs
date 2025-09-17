namespace PropostaFacil.Domain.Users.Contracts;

public interface IUserRuleCheck
{
    bool UserEmailExists(string email);
}

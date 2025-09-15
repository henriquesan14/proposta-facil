namespace PropostaFacil.Domain.Users.Contracts;

public interface IUserContext
{
    Guid GetCurrentUserId();
}
namespace PropostaFacil.Domain.Users.Contracts;

public interface IPasswordHash
{
    string HashPassword(string password);
}
using PropostaFacil.Domain.Users.Contracts;
using System.Text;

namespace PropostaFacil.Infra.Services;

public class PasswordService : IPasswordCheck, IPasswordHash
{
    public bool Matches(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}

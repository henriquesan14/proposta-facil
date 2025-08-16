using PropostaFacil.Domain.Entities;

namespace PropostaFacil.Application.Auth
{
    public interface ITokenService
    {
        TokenResponse GenerateAccessToken(User user);
    }
}

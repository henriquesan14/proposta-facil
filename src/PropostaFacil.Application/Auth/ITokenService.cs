using PropostaFacil.Domain.Users;

namespace PropostaFacil.Application.Auth;

public interface ITokenService
{
    TokenResponse GenerateAccessToken(User user, Guid? impersonateTenantId = null);
}

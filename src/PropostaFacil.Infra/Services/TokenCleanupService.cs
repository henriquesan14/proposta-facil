using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Shared.Interfaces;

namespace PropostaFacil.Infra.Services
{
    public class TokenCleanupService(IUnitOfWork unitOfWork) : ITokenCleanupService
    {
        public async Task CleanupExpiredAndRevokedTokensAsync()
        {
            var now = DateTime.Now;

            var tokensExpireds = await unitOfWork.RefreshTokens
                .GetAsync(t => t.ExpiresAt <= now || t.RevokedAt != null);

            await unitOfWork.RefreshTokens.DeleteRange(tokensExpireds.Select(t => t.Id).ToList());

            await unitOfWork.CompleteAsync();
        }
    }
}

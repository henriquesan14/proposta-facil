using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.RefreshTokens.Specifications;

namespace PropostaFacil.Infra.Services
{
    public class TokenCleanupService(IUnitOfWork unitOfWork) : ITokenCleanupService
    {
        public async Task CleanupExpiredAndRevokedTokensAsync()
        {
            var tokensExpireds = await unitOfWork.RefreshTokens
                .ListAsync(new ListInvalidRefreshTokensSpecification());

            await unitOfWork.RefreshTokens.DeleteRange(tokensExpireds.Select(t => t.Id).ToList());

            await unitOfWork.CompleteAsync();
        }
    }
}

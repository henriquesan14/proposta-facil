using Microsoft.Extensions.Logging;
using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.RefreshTokens.Specifications;

namespace PropostaFacil.Infra.Services;

public class TokenCleanupService(IUnitOfWork unitOfWork, ILogger<TokenCleanupService> logger) : ITokenCleanupService
{
    public async Task CleanupExpiredAndRevokedTokensAsync()
    {
        logger.LogInformation("⏰ Iniciando job de limpeza de tokens revogados e expirados em {Date}", DateTime.Now);
        var tokensExpireds = await unitOfWork.RefreshTokens
            .ListAsync(new ListInvalidRefreshTokensSpecification());

        await unitOfWork.RefreshTokens.DeleteRange(tokensExpireds.Select(t => t.Id).ToList());

        await unitOfWork.CompleteAsync();

        logger.LogInformation("✅ Job de limpeza de tokens revogados e expirados concluído com sucesso ({Count} tokens apagados)", tokensExpireds.Count);
    }
}

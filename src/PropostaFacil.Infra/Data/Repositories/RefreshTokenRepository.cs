using Microsoft.EntityFrameworkCore;
using PropostaFacil.Application.Auth;
using PropostaFacil.Domain.RefreshTokens;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories;

public class RefreshTokenRepository : NoSaveSoftDeleteEfRepository<RefreshToken, RefreshTokenId>, IRefreshTokenRepository
{
    public RefreshTokenRepository(PropostaFacilDbContext dbContext) : base(dbContext)
    {
    }

    public async Task DeleteRange(List<RefreshTokenId> RefreshTokenIds)
    {
        await DbContext.RefreshTokens
        .Where(p => RefreshTokenIds.Contains(p.Id))
        .ExecuteDeleteAsync();
    }
}

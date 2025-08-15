using Microsoft.EntityFrameworkCore;
using PropostaFacil.Application.Auth;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken, RefreshTokenId>, IRefreshTokenRepository
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
}

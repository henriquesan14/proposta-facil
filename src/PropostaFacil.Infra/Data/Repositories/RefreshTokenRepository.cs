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
    }
}

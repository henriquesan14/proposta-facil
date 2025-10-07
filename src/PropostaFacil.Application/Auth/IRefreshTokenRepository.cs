using Ardalis.Specification;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.RefreshTokens;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Auth;

public interface IRefreshTokenRepository : IReadRepositoryBase<RefreshToken>, INoSaveSoftDeleteEfRepository<RefreshToken, RefreshTokenId>
{
    Task DeleteRange(List<RefreshTokenId> RefreshTokenIds);
}

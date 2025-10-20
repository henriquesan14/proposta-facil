using PropostaFacil.Domain.Abstractions;

namespace PropostaFacil.Application.Shared.Interfaces;

public interface INoSaveSoftDeleteEfRepository<TEntity, TId> where TEntity : IAggregate<TId>
{
    Task<TEntity> AddAsync(TEntity entity);
    void Update(TEntity entity);
    void SoftDelete(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
}

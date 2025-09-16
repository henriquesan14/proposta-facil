using PropostaFacil.Domain.Abstractions;

namespace PropostaFacil.Application.Shared.Interfaces;

public interface INoSaveEfRepository<TEntity, TId> where TEntity : IAggregate<TId>
{
    Task<TEntity> AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
}

using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Abstractions;

namespace PropostaFacil.Infra.Data.Repositories;

public class NoSaveSoftDeleteEfRepository<TEntity, TId> : RepositoryBase<TEntity>, INoSaveSoftDeleteEfRepository<TEntity, TId> where TEntity : Entity<TId>, IAggregate<TId>
{
    protected new readonly PropostaFacilDbContext DbContext;

    public NoSaveSoftDeleteEfRepository(PropostaFacilDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await DbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public void Update(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public void SoftDelete(TEntity entity)
    {
        entity.IsActive = false;

        DbContext.Entry(entity).Property(e => e.IsActive).IsModified = true;
    }
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbContext.Set<TEntity>().AddRangeAsync(entities);
    }
}

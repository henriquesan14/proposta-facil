using Microsoft.EntityFrameworkCore.Storage;
using PropostaFacil.Application.Contracts.Data;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction _transaction;
        private readonly PropostaFacilDbContext _dbContext;

        public UnitOfWork(PropostaFacilDbContext dbContext, ITenantRepository tenants)
        {
            _dbContext = dbContext;
            Tenants = tenants;
        }

        public ITenantRepository Tenants { get; }

        public async Task BeginTransaction()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }

        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            IsDisposing(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void IsDisposing(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}

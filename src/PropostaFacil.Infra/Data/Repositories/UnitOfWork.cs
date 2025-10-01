using Microsoft.EntityFrameworkCore.Storage;
using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Clients;
using PropostaFacil.Application.Payments;
using PropostaFacil.Application.Proposals;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.SubscriptionPlans;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Application.Users;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction _transaction;
        private readonly PropostaFacilDbContext _dbContext;

        public UnitOfWork(PropostaFacilDbContext dbContext, ITenantRepository tenants, IClientRepository clients, IProposalRepository proposals, IUserRepository users, IRefreshTokenRepository refreshTokens, ISubscriptionRepository subscriptions, ISubscriptionPlanRepository subscriptionPlans, IPaymentRepository payments)
        {
            _dbContext = dbContext;
            Tenants = tenants;
            Clients = clients;
            Proposals = proposals;
            Users = users;
            RefreshTokens = refreshTokens;
            Subscriptions = subscriptions;
            SubscriptionPlans = subscriptionPlans;
            Payments = payments;
        }

        public ITenantRepository Tenants { get; }
        public IClientRepository Clients { get; }
        public IProposalRepository Proposals { get; }
        public IUserRepository Users { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public ISubscriptionRepository Subscriptions { get; }
        public ISubscriptionPlanRepository SubscriptionPlans { get; }
        public IPaymentRepository Payments { get; }

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

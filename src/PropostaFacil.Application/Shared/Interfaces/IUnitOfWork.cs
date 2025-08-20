using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Clients;
using PropostaFacil.Application.Proposals;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Application.Users;

namespace PropostaFacil.Application.Shared.Interfaces
{
    public interface IUnitOfWork
    {
        ITenantRepository Tenants { get; }
        IClientRepository Clients { get; }
        IProposalRepository Proposals { get; }
        IUserRepository Users { get; }
        IRefreshTokenRepository RefreshTokens { get; }

        ISubscriptionRepository Subscriptions { get; }

        ISubscriptionPlanRepository SubscriptionPlans { get; }
        Task<int> CompleteAsync();
        Task BeginTransaction();
        Task CommitAsync();
    }
}

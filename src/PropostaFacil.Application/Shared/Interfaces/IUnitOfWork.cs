using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Clients;
using PropostaFacil.Application.Payments;
using PropostaFacil.Application.Proposals;
using PropostaFacil.Application.SubscriptionPlans;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Application.Users;

namespace PropostaFacil.Application.Shared.Interfaces;

public interface IUnitOfWork
{
    ITenantRepository Tenants { get; }
    IClientRepository Clients { get; }
    IProposalRepository Proposals { get; }
    IUserRepository Users { get; }
    IRefreshTokenRepository RefreshTokens { get; }

    ISubscriptionRepository Subscriptions { get; }
    ISubscriptionPlanRepository SubscriptionPlans { get; }
    IPaymentRepository Payments { get; }
    Task<int> CompleteAsync();
    Task BeginTransaction();
    Task CommitAsync();
}

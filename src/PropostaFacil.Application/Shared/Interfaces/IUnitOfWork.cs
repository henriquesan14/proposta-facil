using PropostaFacil.Application.Clients;
using PropostaFacil.Application.Proposals;
using PropostaFacil.Application.Tenants;

namespace PropostaFacil.Application.Shared.Interfaces
{
    public interface IUnitOfWork
    {
        ITenantRepository Tenants { get; }
        IClientRepository Clients { get; }

        IProposalRepository Proposals { get; }
        Task<int> CompleteAsync();
        Task BeginTransaction();
        Task CommitAsync();
    }
}

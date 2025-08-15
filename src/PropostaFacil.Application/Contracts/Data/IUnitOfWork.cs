namespace PropostaFacil.Application.Contracts.Data
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

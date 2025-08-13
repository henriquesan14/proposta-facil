namespace PropostaFacil.Application.Contracts.Data
{
    public interface IUnitOfWork
    {
        ITenantRepository Tenants { get; }
        IClientRepository Clients { get; }
        Task<int> CompleteAsync();
        Task BeginTransaction();
        Task CommitAsync();
    }
}

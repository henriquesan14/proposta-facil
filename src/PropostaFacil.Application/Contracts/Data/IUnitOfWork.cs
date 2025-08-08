namespace PropostaFacil.Application.Contracts.Data
{
    public interface IUnitOfWork
    {
        ITenantRepository Tenants { get; }
        Task<int> CompleteAsync();
        Task BeginTransaction();
        Task CommitAsync();
    }
}

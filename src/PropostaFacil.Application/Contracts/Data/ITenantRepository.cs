using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects;

namespace PropostaFacil.Application.Contracts.Data
{
    public interface ITenantRepository : IAsyncRepository<Tenant, TenantId>
    {
    }
}

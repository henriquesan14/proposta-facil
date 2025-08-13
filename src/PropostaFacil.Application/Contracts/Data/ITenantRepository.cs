using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Contracts.Data
{
    public interface ITenantRepository : IAsyncRepository<Tenant, TenantId>
    {
    }
}

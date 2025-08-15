using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Tenants
{
    public interface ITenantRepository : IAsyncRepository<Tenant, TenantId>;
}

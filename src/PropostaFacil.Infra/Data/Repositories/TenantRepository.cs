using PropostaFacil.Application.Tenants;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories;

public class TenantRepository : NoSaveSoftDeleteEfRepository<Tenant, TenantId>, ITenantRepository
{
    public TenantRepository(PropostaFacilDbContext dbContext) : base(dbContext)
    {
    }
}

using PropostaFacil.Application.Tenants;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class TenantRepository : RepositoryBase<Tenant, TenantId>, ITenantRepository
    {
        public TenantRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}

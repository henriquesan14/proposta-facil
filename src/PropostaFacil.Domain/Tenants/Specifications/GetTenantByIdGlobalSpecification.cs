using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Tenants.Specifications;

public class GetTenantByIdGlobalSpecification : GlobalSingleResultSpecification<Tenant, TenantId>
{
    public GetTenantByIdGlobalSpecification(TenantId id)
    {
        Query
            .Where(t => t.Id == id);
    }
}

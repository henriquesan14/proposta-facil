using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Users.Specifications;

public class GetUsersByTenantIdGlobalSpecification : GlobalSpecification<User, UserId>
{
    public GetUsersByTenantIdGlobalSpecification(TenantId tenantId)
    {
        Query.Where(u => u.TenantId == tenantId && u.IsActive);
    }
}

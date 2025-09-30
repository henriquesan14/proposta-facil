using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Tenants.Specifications;

public class GetTenantByEmailGlobalSpecification : GlobalSingleResultSpecification<Tenant, TenantId>
{
    public GetTenantByEmailGlobalSpecification(string email)
    {
        Query
            .Where(x => x.Contact.Email.Equals(email));
    }
}

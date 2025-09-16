using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Tenants.Specifications;

public class GetTenantByDocumentGlobalSpecification : GlobalSingleResultSpecification<Tenant, TenantId>
{
    public GetTenantByDocumentGlobalSpecification(string document)
    {
        Query
            .Where(t => t.Document.Number == document);
    }
}

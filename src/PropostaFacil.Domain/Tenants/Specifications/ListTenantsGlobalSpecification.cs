using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Tenants.Specifications;

public class ListTenantsGlobalSpecification : GlobalSpecification<Tenant, TenantId>
{
    public ListTenantsGlobalSpecification(string? name, string? document)
    {
        Query
            .Where(
                p =>
                (string.IsNullOrEmpty(name) ||
                    p.Name.ToLower().Contains(name.ToLower())) &&
                (string.IsNullOrEmpty(document) ||
                    p.Document.Number == document
            ));
    }
}

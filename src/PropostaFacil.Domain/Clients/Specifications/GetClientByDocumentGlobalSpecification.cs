using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Clients.Specifications;

public class GetClientByDocumentGlobalSpecification : GlobalSingleResultSpecification<Client, ClientId>
{
    public GetClientByDocumentGlobalSpecification(string document)
    {
        Query
        .Where(t => t.Document.Number == document && t.IsActive);
    }
}

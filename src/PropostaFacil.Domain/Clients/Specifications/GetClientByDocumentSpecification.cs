using Ardalis.Specification;

namespace PropostaFacil.Domain.Clients.Specifications;

public class GetClientByDocumentSpecification : SingleResultSpecification<Client>
{
    public GetClientByDocumentSpecification(string document)
    {
        Query
        .Where(t => t.Document.Number == document && t.IsActive);
    }
}
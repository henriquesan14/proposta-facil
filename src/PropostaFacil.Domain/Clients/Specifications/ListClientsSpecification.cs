using Ardalis.Specification;

namespace PropostaFacil.Domain.Clients.Specifications;

public class ListClientsSpecification : Specification<Client>
{
    public ListClientsSpecification(string? name, string? document)
    {
        Query
            .Where(p => (string.IsNullOrEmpty(name) ||
                    p.Name.ToLower().Contains(name.ToLower())) &&
                (string.IsNullOrEmpty(document) ||
                    p.Document.Number == document));
    }
}

using Ardalis.Specification;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Clients.Specifications;

public class GetClientByIdSpecification : SingleResultSpecification<Client>
{
    public GetClientByIdSpecification(ClientId Id)
    {
        Query
            .Where(c => c.Id == Id && c.IsActive);
    }
}

using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Clients.Specifications;

public class GetClientByEmailGlobalSpecification : GlobalSingleResultSpecification<Client, ClientId>
{
    public GetClientByEmailGlobalSpecification(string email)
    {
        Query
            .Where(c => c.Contact.Email == email);
    }
}

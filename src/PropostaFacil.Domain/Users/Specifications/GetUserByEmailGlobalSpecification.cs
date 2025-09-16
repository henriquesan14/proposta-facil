using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Users.Specifications;

public class GetUserByEmailGlobalSpecification : GlobalSingleResultSpecification<User, UserId>
{
    public GetUserByEmailGlobalSpecification(string email)
    {
        Query
            .Where(x => x.Contact.Email.Equals(email));
    }
}
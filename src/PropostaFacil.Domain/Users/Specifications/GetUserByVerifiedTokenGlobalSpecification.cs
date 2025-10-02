using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Users.Specifications;

public class GetUserByVerifiedTokenGlobalSpecification : GlobalSingleResultSpecification<User, UserId>
{
    public GetUserByVerifiedTokenGlobalSpecification(string token)
    {
        Query
            .Where(u => u.VerifiedToken == token);
    }
}

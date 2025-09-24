using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Users.Specifications;

public class GetUserByIdGlobalSpecification : GlobalSingleResultSpecification<User, UserId>
{
    public GetUserByIdGlobalSpecification(UserId userId)
    {
        Query
            .Where(u => u.Id == userId);
    }
}

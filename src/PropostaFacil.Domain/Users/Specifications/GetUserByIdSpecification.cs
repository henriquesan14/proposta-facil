using Ardalis.Specification;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Users.Specifications;

public class GetUserByIdSpecification : SingleResultSpecification<User>
{
    public GetUserByIdSpecification(UserId userId)
    {
        Query
            .Where(u => u.Id == userId && u.IsActive);
    }
}

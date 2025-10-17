using Ardalis.Specification;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Users.Specifications;

public class GetUserAdminSystemSpecification : GlobalSingleResultSpecification<User, UserId>
{
    public GetUserAdminSystemSpecification()
    {
        Query
            .Where(u => u.Role == Enums.UserRoleEnum.AdminSystem);
    }
}

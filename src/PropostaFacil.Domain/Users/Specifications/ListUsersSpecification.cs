using Ardalis.Specification;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Domain.Users.Specifications;

public class ListUsersSpecification : Specification<User>
{
    public ListUsersSpecification(string? name, UserRoleEnum? role, bool onlyActive)
    {
        Query
            .Where(p =>
            (!onlyActive || p.IsActive) &&
            (string.IsNullOrEmpty(name) ||
                    p.Name.ToLower().Contains(name.ToLower())) &&
                    (!role.HasValue || p.Role == role))
            .OrderByDescending(p => p.CreatedAt);
    }
}

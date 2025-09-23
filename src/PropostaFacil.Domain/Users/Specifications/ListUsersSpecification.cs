using Ardalis.Specification;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Domain.Users.Specifications;

public class ListUsersSpecification : Specification<User>
{
    public ListUsersSpecification(string? name, UserRoleEnum? role)
    {
        Query
            .Where(p => (string.IsNullOrEmpty(name) ||
                    p.Name.ToLower().Contains(name.ToLower())) &&
                    (!role.HasValue || p.Role == role))
            .OrderByDescending(p => p.CreatedAt);
    }
}

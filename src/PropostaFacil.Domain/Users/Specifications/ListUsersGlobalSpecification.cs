using Ardalis.Specification;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Users.Specifications;

public class ListUsersGlobalSpecification : GlobalSpecification<User, UserId>
{
    public ListUsersGlobalSpecification(TenantId? tenantId, string? name, UserRoleEnum? role, bool onlyActive)
    {
        Query
            .Where(p =>
            (!onlyActive || p.IsActive) &&
            (tenantId == null || p.TenantId == tenantId) &&
            (string.IsNullOrEmpty(name) ||
                    p.Name.ToLower().Contains(name.ToLower())) &&
                    (!role.HasValue || p.Role == role))
            .OrderByDescending(p => p.CreatedAt);
    }
}

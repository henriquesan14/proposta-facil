using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Entities
{
    public class User : Aggregate<UserId>
    {
        public string Name { get; private set; } = default!;
        public Contact Contact { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public UserRoleEnum Role { get; private set; } = default!;

        public TenantId TenantId { get; private set; } = default!;
        public Tenant Tenant { get; private set; } = default!;

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
        public void ChangePassword(string newHash) => PasswordHash = newHash;
        public void ChangeRole(UserRoleEnum newRole) => Role = newRole;
    }
}

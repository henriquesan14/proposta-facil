using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Tests.Builders.Entities
{
    public class UserBuilder
    {
        private string _name = "Default User";
        private Contact _contact = Contact.Of("user@example.com", "11999999999");
        private string _passwordHash = BCrypt.Net.BCrypt.HashPassword("Password123", 8);
        private UserRoleEnum _role = UserRoleEnum.AdminTenant;
        private TenantId? _tenantId = TenantId.Of(Guid.NewGuid());

        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UserBuilder WithContact(Contact contact)
        {
            _contact = contact;
            return this;
        }

        public UserBuilder WithPasswordHash(string passwordHash)
        {
            _passwordHash = passwordHash;
            return this;
        }

        public UserBuilder WithRole(UserRoleEnum role)
        {
            _role = role;
            return this;
        }

        public UserBuilder WithTenantId(TenantId? tenantId)
        {
            _tenantId = tenantId;
            return this;
        }

        public User Build()
        {
            return User.Create(_name, _contact, _passwordHash, _role, _tenantId!);
        }
    }

}

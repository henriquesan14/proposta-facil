using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Tests.Builders.Entities
{
    public class UserBuilder
    {
        private string _name = "Default User";
        private Contact _contact = Contact.Of("user@example.com", "11999999999");
        private string _password = "Password123";
        private UserRoleEnum _role = UserRoleEnum.AdminTenant;
        private TenantId? _tenantId = TenantId.Of(Guid.NewGuid());
        private IPasswordHash _passwordHash;

        public UserBuilder(IPasswordHash passwordHash)
        {
            _passwordHash = passwordHash;
        }

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

        public UserBuilder WithPassword(string password)
        {
            _password = password;
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
            return User.Create(_name, _contact, _password, _role, _tenantId!, _passwordHash);
        }
    }

}

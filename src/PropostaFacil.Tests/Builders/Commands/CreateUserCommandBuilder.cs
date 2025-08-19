using PropostaFacil.Application.Users.Commands.CreateUser;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Tests.Builders.Commands
{
    public class CreateUserCommandBuilder
    {
        private string _name = "John Doe";
        private string _email = "john@email.com";
        private string _phone = "11999999999";
        private string _password = "123456";
        private UserRoleEnum _role = UserRoleEnum.AdminTenant;
        private Guid? _tenantId = Guid.NewGuid();

        public CreateUserCommand Build()
            => new CreateUserCommand(_name, _email, _phone, _password, _role, _tenantId);

        public CreateUserCommandBuilder WithRole(UserRoleEnum role)
        {
            _role = role;
            return this;
        }

        public CreateUserCommandBuilder WithTenantId(Guid? tenantId)
        {
            _tenantId = tenantId;
            return this;
        }

        public CreateUserCommandBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
    }
}

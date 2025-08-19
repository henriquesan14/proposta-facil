using PropostaFacil.Application.Users.Queries.GetUsers;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Tests.Builders.Queries
{
    public class GetUsersQueryBuilder
    {
        private Guid _tenantId = Guid.NewGuid();
        private string _name = "Henrique";
        private UserRoleEnum _role = UserRoleEnum.AdminSystem;

        public GetUsersQuery Build() => new GetUsersQuery(_tenantId, _name, _role);

        public GetUsersQueryBuilder WithTenantId(Guid tenantId)
        {
            this._tenantId = tenantId;
            return this;
        }

        public GetUsersQueryBuilder WithName(String name)
        {
            this._name = name;
            return this;
        }

        public GetUsersQueryBuilder WithRole(UserRoleEnum role)
        {
            this._role = role;
            return this;
        }
    }
}

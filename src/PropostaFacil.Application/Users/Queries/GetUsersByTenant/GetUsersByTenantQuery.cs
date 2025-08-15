using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Queries.GetUsersByTenant
{
    public record GetUsersByTenantQuery(Guid TenantId) : IQuery<ResultT<IEnumerable<UserResponse>>>;
}

using Common.ResultPattern;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Users.Queries.GetUsers
{
    public record GetUsersQuery(Guid? TenantId, string? Name, UserRoleEnum? Role, int PageNumber = 1, int PageSize = 20) : IQuery<ResultT<PaginatedResult<UserResponse>>>;
}

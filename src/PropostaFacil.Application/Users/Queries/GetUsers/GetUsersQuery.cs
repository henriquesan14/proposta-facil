using Common.ResultPattern;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Users.Queries.GetUsers
{
    public record GetUsersQuery(string? Name, UserRoleEnum? Role, int PageIndex = 1, int PageSize = 10) : IQuery<ResultT<PaginatedResult<UserResponse>>>;
}

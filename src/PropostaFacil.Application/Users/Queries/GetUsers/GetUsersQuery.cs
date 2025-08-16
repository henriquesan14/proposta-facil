using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Users.Queries.GetUsers
{
    public record GetUsersQuery(PaginationRequest PaginationRequest) : IQuery<ResultT<PaginatedResult<UserResponse>>>;
}

using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetUsersQuery, ResultT<PaginatedResult<UserResponse>>>
    {
        public async Task<ResultT<PaginatedResult<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var spec = new ListUsersSpecification(request.Name, request.Role);
            var paginated = await unitOfWork.Users
                .ToPaginatedListAsync(spec, request.PageIndex, request.PageSize, u => u.ToDto());

            return paginated;
        }
    }
}

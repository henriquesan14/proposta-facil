using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Users.Queries.AdminGetUsers;

public class AdminGetUsersQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<AdminGetUsersQuery, ResultT<PaginatedResult<UserResponse>>>
{
    public async Task<ResultT<PaginatedResult<UserResponse>>> Handle(AdminGetUsersQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId.HasValue ? TenantId.Of(request.TenantId.Value) : null;
        var spec = new ListUsersGlobalSpecification(tenantId, request.Name, request.Role, request.OnlyActive);
        var paginated = await unitOfWork.Users
            .ToPaginatedListAsync(spec, request.PageIndex, request.PageSize, u => u.ToDto());

        return paginated;
    }
}

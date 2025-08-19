using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;
using System.Linq.Expressions;

namespace PropostaFacil.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IQueryHandler<GetUsersQuery, ResultT<PaginatedResult<UserResponse>>>
    {
        public async Task<ResultT<PaginatedResult<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> users;
            int count;

            if (currentUserService.Role == UserRoleEnum.AdminSystem)
            {
                Expression<Func<User, bool>> predicate = p =>
                (!request.TenantId.HasValue || p.TenantId == TenantId.Of(request.TenantId.Value)) &&
                (string.IsNullOrEmpty(request.Name) ||
                    p.Name.ToLower().Contains(request.Name.ToLower())) &&
                (!request.Role.HasValue ||
                    p.Role == request.Role);

                users = await unitOfWork.Users.GetAsync(
                    predicate: predicate,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize
                );

                count = await unitOfWork.Users.GetCountAsync();
            }
            else
            {
                var tenantId = TenantId.Of(currentUserService.TenantId!.Value);
                Expression<Func<User, bool>> predicate = p =>
                (p.TenantId == tenantId) &&
                (string.IsNullOrEmpty(request.Name) ||
                    p.Name.ToLower().Contains(request.Name.ToLower())) &&
                (!request.Role.HasValue ||
                    p.Role == request.Role);

                users = await unitOfWork.Users.GetAsync(
                    u => u.TenantId == tenantId,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize
                );

                count = await unitOfWork.Users.GetCountAsync(predicate);
            }

            var dto = users.ToDto();

            var paginated = new PaginatedResult<UserResponse>(
                request.PageNumber,
                request.PageSize,
                count,
                dto
            );

            return paginated;
        }
    }
}

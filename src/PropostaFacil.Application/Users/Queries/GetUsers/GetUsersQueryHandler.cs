using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

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
                users = await unitOfWork.Users.GetAsync(
                    pageNumber: request.PaginationRequest.PageIndex,
                    pageSize: request.PaginationRequest.PageSize
                );

                count = await unitOfWork.Users.GetCountAsync();
            }
            else
            {
                var tenantId = TenantId.Of(currentUserService.TenantId!.Value);

                users = await unitOfWork.Users.GetAsync(
                    u => u.TenantId == tenantId,
                    pageNumber: request.PaginationRequest.PageIndex,
                    pageSize: request.PaginationRequest.PageSize
                );

                count = await unitOfWork.Users.GetCountAsync(u => u.TenantId == tenantId);
            }

            var dto = users.ToDto();

            var paginated = new PaginatedResult<UserResponse>(
                request.PaginationRequest.PageIndex,
                request.PaginationRequest.PageSize,
                count,
                dto
            );

            return paginated;
        }
    }
}

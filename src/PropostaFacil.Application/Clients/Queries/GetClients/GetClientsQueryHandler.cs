using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Clients.Queries.GetClients
{
    public class GetClientsQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IQueryHandler<GetClientsQuery, ResultT<PaginatedResult<ClientResponse>>>
    {
        public async Task<ResultT<PaginatedResult<ClientResponse>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Client> clients;
            int count;

            if (currentUserService.Role == UserRoleEnum.AdminSystem)
            {
                clients = await unitOfWork.Clients.GetAsync(
                    pageNumber: request.PaginationRequest.PageIndex,
                    pageSize: request.PaginationRequest.PageSize
                );

                count = await unitOfWork.Clients.GetCountAsync();
            }
            else
            {
                var tenantId = TenantId.Of(currentUserService.TenantId!.Value);

                clients = await unitOfWork.Clients.GetAsync(
                    u => u.TenantId == tenantId,
                    pageNumber: request.PaginationRequest.PageIndex,
                    pageSize: request.PaginationRequest.PageSize
                );

                count = await unitOfWork.Clients.GetCountAsync(u => u.TenantId == tenantId);
            }

            var dto = clients.ToDto();

            var paginated = new PaginatedResult<ClientResponse>(
                request.PaginationRequest.PageIndex,
                request.PaginationRequest.PageSize,
                count,
                dto
            );

            return paginated;
        }
    }
}

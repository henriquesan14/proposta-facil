using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;
using System.Linq.Expressions;

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
                Expression<Func<Client, bool>> predicate = p =>
                (!request.TenantId.HasValue || p.TenantId == TenantId.Of(request.TenantId.Value)) &&
                (string.IsNullOrEmpty(request.Name) ||
                    p.Name.ToLower().Contains(request.Name.ToLower())) &&
                (!string.IsNullOrEmpty(request.Document) ||
                    p.Document.Number == request.Document);
                clients = await unitOfWork.Clients.GetAsync(
                    predicate: predicate,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize
                );

                count = await unitOfWork.Clients.GetCountAsync();
            }
            else
            {
                var tenantId = TenantId.Of(currentUserService.TenantId!.Value);
                Expression<Func<Client, bool>> predicate = p =>
                (p.TenantId == tenantId) &&
                (string.IsNullOrEmpty(request.Name) ||
                    p.Name.ToLower().Contains(request.Name.ToLower())) &&
                (!string.IsNullOrEmpty(request.Document) ||
                    p.Document.Number == request.Document);
                clients = await unitOfWork.Clients.GetAsync(
                    predicate,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize
                );

                count = await unitOfWork.Clients.GetCountAsync(u => u.TenantId == tenantId);
            }

            var dto = clients.ToDto();

            var paginated = new PaginatedResult<ClientResponse>(
                request.PageNumber,
                request.PageSize,
                count,
                dto
            );

            return paginated;
        }
    }
}

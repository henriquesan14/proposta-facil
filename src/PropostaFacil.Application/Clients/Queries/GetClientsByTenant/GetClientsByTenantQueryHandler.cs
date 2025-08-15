using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Queries.GetClientsByTenant
{
    public class GetClientsByTenantQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IQueryHandler<GetClientsByTenantQuery, ResultT<IEnumerable<ClientResponse>>>
    {
        public async Task<ResultT<IEnumerable<ClientResponse>>> Handle(GetClientsByTenantQuery request, CancellationToken cancellationToken)
        {
            var clients = await unitOfWork.Clients.GetAsync(c => c.TenantId == TenantId.Of(currentUserService.TenantId));

            return clients.ToDto();
        }
    }
}

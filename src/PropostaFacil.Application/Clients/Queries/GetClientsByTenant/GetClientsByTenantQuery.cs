using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Queries.GetClientsByTenant
{
    public record GetClientsByTenantQuery(Guid TenantId) : IQuery<ResultT<IEnumerable<ClientResponse>>>;
}

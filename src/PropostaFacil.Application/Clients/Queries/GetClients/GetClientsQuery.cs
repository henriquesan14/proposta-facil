using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Clients.Queries.GetClients
{
    public record GetClientsQuery(string? Name, string? Document, Guid? TenantId, int PageNumber =1, int PageSize = 20) : IQuery<ResultT<PaginatedResult<ClientResponse>>>;
}

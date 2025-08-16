using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Clients.Queries.GetClients
{
    public record GetClientsQuery(PaginationRequest PaginationRequest) : IQuery<ResultT<PaginatedResult<ClientResponse>>>;
}

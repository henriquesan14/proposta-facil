using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Clients.Queries.GetClients;

public record GetClientsQuery(string? Name, string? Document, int PageIndex =1, int PageSize = 10) : IQuery<ResultT<PaginatedResult<ClientResponse>>>;

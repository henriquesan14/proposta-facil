using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Queries.GetClientById;

public record GetClientByIdQuery(Guid Id) : IQuery<ResultT<ClientResponse>>;

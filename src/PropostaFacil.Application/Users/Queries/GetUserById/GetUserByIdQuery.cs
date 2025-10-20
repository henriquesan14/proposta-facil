using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<ResultT<UserResponse>>;

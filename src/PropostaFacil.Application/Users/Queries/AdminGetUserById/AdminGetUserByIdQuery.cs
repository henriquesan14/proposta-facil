using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Queries.AdminGetUserById;

public record AdminGetUserByIdQuery(Guid Id) : IQuery<ResultT<UserResponse>>;

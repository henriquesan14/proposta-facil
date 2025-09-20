using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.RevokeRefreshToken;

public record RevokeRefreshTokenCommand : ICommand<Result>;

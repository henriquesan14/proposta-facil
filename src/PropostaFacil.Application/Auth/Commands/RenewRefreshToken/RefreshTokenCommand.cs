using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.RenewRefreshToken;

public record RefreshTokenCommand : ICommand<ResultT<AuthResponse>>;

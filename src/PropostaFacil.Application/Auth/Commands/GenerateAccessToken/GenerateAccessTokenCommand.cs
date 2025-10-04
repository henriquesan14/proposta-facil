using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.GenerateAccessToken;

public record GenerateAccessTokenCommand(string Email, string Password) : ICommand<ResultT<AuthResponse>>;

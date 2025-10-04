using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.ResetPassword;

public record ResetPasswordCommand(Guid UserId, string Token, string Password) : ICommand<Result>;

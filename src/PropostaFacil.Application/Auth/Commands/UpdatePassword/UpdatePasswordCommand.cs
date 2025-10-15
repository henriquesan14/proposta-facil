using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.UpdatePassword;

public record UpdatePasswordCommand(string OldPassword, string NewPassword) : ICommand<Result>;

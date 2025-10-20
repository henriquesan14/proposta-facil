using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : ICommand<Result>;

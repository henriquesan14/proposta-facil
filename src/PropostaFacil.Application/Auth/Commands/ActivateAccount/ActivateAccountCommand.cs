using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.ActivateAccount;

public record ActivateAccountCommand(string Token, string Password) : ICommand<Result>;

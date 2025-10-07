using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : ICommand<Result>;

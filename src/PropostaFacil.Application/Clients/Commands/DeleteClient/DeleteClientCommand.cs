using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Commands.DeleteClient;

public record DeleteClientCommand(Guid Id) : ICommand<Result>;

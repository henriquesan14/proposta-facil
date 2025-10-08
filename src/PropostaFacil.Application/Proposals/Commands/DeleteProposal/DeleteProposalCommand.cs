using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.DeleteProposal;

public record DeleteProposalCommand(Guid Id) : ICommand<Result>;

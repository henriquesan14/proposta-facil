using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.SendProposal
{
    public record SendProposalCommand(Guid ProposalId) : ICommand<Result>;
}

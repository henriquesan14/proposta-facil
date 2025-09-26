using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.CreateProposal
{
    public record CreateProposalCommand(Guid ClientId, string Title,
        string Currency, DateTime ValidUntil, IEnumerable<ProposalItemRequest> Items) : ICommand<ResultT<ProposalResponse>>;
}

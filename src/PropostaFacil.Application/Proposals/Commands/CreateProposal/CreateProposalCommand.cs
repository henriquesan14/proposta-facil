using Common.ResultPattern;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.CreateProposal
{
    public record CreateProposalCommand(Guid ClientId, string Number, string Title, ProposalStatusEnum ProposalStatus,
        string Currency, DateTime ValidUntil, IEnumerable<ProposalItemRequest> Items, Guid? TenantId) : ICommand<ResultT<ProposalResponse>>;
}

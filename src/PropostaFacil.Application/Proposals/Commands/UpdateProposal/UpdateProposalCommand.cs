using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.UpdateProposal;

public record UpdateProposalCommand(Guid Id, Guid ClientId, string Title, string Currency, DateTime ValidUntil, IEnumerable<ProposalItemUpdateRequest> Items) : ICommand<Result>;

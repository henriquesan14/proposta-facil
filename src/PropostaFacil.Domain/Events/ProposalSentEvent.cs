using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Proposals;

namespace PropostaFacil.Domain.Events
{
    public record ProposalSentEvent(Proposal Proposal) : IDomainEvent;
}

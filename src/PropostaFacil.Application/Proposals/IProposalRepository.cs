using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Proposals
{
    public interface IProposalRepository : IAsyncRepository<Proposal, ProposalId>;
}

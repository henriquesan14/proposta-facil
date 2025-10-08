using Ardalis.Specification;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Proposals;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Proposals;

public interface IProposalRepository : IReadRepositoryBase<Proposal>, INoSaveSoftDeleteEfRepository<Proposal, ProposalId>
{
    Task SoftDeleteProposalItems(ProposalId proposalId);
}
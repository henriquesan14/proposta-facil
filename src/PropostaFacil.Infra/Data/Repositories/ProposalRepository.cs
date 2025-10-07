using PropostaFacil.Application.Proposals;
using PropostaFacil.Domain.Proposals;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories;

public class ProposalRepository : NoSaveSoftDeleteEfRepository<Proposal, ProposalId>, IProposalRepository
{
    public ProposalRepository(PropostaFacilDbContext dbContext) : base(dbContext)
    {
    }
}

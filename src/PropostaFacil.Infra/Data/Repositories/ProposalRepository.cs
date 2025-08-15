using PropostaFacil.Application.Proposals;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class ProposalRepository : RepositoryBase<Proposal, ProposalId>, IProposalRepository
    {
        public ProposalRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PropostaFacil.Application.Proposals;
using PropostaFacil.Domain.Proposals;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories;

public class ProposalRepository : NoSaveSoftDeleteEfRepository<Proposal, ProposalId>, IProposalRepository
{
    public ProposalRepository(PropostaFacilDbContext dbContext) : base(dbContext)
    {
    }
    public async Task SoftDeleteProposalItems(ProposalId proposalId)
    {
        await DbContext.ProposalItems
            .Where(p => p.ProposalId == proposalId && p.IsActive)
            .ExecuteUpdateAsync(p => p.SetProperty(x => x.IsActive, false));
    }
}

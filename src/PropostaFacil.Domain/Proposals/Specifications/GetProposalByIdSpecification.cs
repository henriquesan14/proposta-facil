using Ardalis.Specification;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Proposals.Specifications;

public class GetProposalByIdSpecification : SingleResultSpecification<Proposal>
{
    public GetProposalByIdSpecification(ProposalId id)
    {
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Client)
            .Include(p => p.Items);
    }
}

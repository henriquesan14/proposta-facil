using Ardalis.Specification;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Proposals.Specifications;

public class ListProposalsSpecification : Specification<Proposal>
{
    public ListProposalsSpecification(Guid? clientId, string? number, string? title, ProposalStatusEnum? proposalStatus)
    {
        Query
            .Where(p => (!clientId.HasValue || p.ClientId == ClientId.Of(clientId.Value)) &&
                 (string.IsNullOrEmpty(number) ||
                    p.Number == number) &&
                (string.IsNullOrEmpty(title) ||
                    p.Title.ToLower().Contains(title.ToLower())) &&
                (!proposalStatus.HasValue || p.ProposalStatus == proposalStatus))
            .Include(p => p.Items);
    }
}

using Ardalis.Specification;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Domain.Proposals.Specifications;

public class ListProposalsSpecification : Specification<Proposal>
{
    public ListProposalsSpecification(string? documentClient, string? number, string? title, ProposalStatusEnum? proposalStatus)
    {
        Query
            .Where(p => (string.IsNullOrEmpty(documentClient) || p.Client.Document.Number == documentClient) &&
                 (string.IsNullOrEmpty(number) ||
                    p.Number == number) &&
                (string.IsNullOrEmpty(title) ||
                    p.Title.ToLower().Contains(title.ToLower())) &&
                (!proposalStatus.HasValue || p.ProposalStatus == proposalStatus))
            .Include(p => p.Client)
            .Include(p => p.Items);
    }
}

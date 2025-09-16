using PropostaFacil.Domain.Proposals;

namespace PropostaFacil.Application.Proposals
{
    public static class ProposalExtensions
    {
        public static ProposalResponse ToDto(this Proposal proposal)
        {
            return new ProposalResponse(
                proposal.Id.Value,
                proposal.TenantId.Value,
                proposal.ClientId.Value,
                proposal.Number,
                proposal.Title,
                proposal.ProposalStatus,
                proposal.TotalAmount.ToString(),
                proposal.ValidUntil,
                proposal.Items.ToDto()
            );
        }

        public static List<ProposalResponse> ToDto(this IEnumerable<Proposal> proposals)
        {
            return proposals
                .Select(ToDto)
                .ToList();
        }
    }
}

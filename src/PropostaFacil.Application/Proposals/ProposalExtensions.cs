using PropostaFacil.Application.Clients;
using PropostaFacil.Domain.Proposals;

namespace PropostaFacil.Application.Proposals;

public static class ProposalExtensions
{
    public static ProposalResponse ToDto(this Proposal proposal)
    {
        return new ProposalResponse(
            proposal.Id.Value,
            proposal.TenantId.Value,
            proposal.Client?.ToDto()!,
            proposal.Number,
            proposal.Title,
            proposal.ProposalStatus,
            proposal.TotalAmount.Currency,
            proposal.TotalAmount.Amount,
            proposal.ValidUntil,
            proposal.Items.ToDto(),
            proposal.IsActive,
            proposal.CreatedAt
        );
    }

    public static List<ProposalResponse> ToDto(this IEnumerable<Proposal> proposals)
    {
        return proposals
            .Select(ToDto)
            .ToList();
    }
}

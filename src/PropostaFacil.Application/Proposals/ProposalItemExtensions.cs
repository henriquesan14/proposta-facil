using PropostaFacil.Domain.Proposals;

namespace PropostaFacil.Application.Proposals;

public static class ProposalItemExtensions
{
    public static ProposalItemResponse ToDto(this ProposalItem proposalItem)
    {
        return new ProposalItemResponse(
            proposalItem.Id.Value,
            proposalItem.Name,
            proposalItem.Description,
            proposalItem.Quantity,
            proposalItem.UnitPrice,
            proposalItem.TotalPrice
        );
    }

    public static List<ProposalItemResponse> ToDto(this IEnumerable<ProposalItem> proposalItems)
    {
        return proposalItems
            .Select(ToDto)
            .ToList();
    }
}

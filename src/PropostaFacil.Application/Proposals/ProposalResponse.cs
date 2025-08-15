using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Application.Proposals
{
    public record ProposalResponse(Guid Id, Guid TenantId, Guid ClientId, string Number, string Title, ProposalStatusEnum ProposalStatus,  string TotalAmount,
        DateTime ValidUntil, IEnumerable<ProposalItemResponse> Items);
}

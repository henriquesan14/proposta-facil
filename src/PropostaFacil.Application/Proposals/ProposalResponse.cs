using PropostaFacil.Application.Clients;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Application.Proposals
{
    public record ProposalResponse(Guid Id, Guid TenantId, ClientResponse Client, string Number, string Title, ProposalStatusEnum ProposalStatus,  string TotalAmount,
        DateTime ValidUntil, IEnumerable<ProposalItemResponse> Items);
}

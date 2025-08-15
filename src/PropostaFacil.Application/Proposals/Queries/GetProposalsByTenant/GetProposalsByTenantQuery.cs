using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Queries.GetProposalsByTenant
{
    public record GetProposalsByTenantQuery(Guid TenantId) : IQuery<ResultT<IEnumerable<ProposalResponse>>>;
}

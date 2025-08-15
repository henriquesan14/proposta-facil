using Common.ResultPattern;
using PropostaFacil.Application.Contracts.Data;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Queries.GetProposalsByTenant
{
    public class GetProposalsByTenantQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetProposalsByTenantQuery, ResultT<IEnumerable<ProposalResponse>>>
    {
        public async Task<ResultT<IEnumerable<ProposalResponse>>> Handle(GetProposalsByTenantQuery request, CancellationToken cancellationToken)
        {
            var proposals = await unitOfWork.Proposals.GetAsync(p => p.TenantId == TenantId.Of(request.TenantId), includeString: "Items");

            return proposals.ToDto();
        }
    }
}

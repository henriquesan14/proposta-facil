using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Queries.GetProposalsByTenant
{
    public class GetProposalsByTenantQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IQueryHandler<GetProposalsByTenantQuery, ResultT<IEnumerable<ProposalResponse>>>
    {
        public async Task<ResultT<IEnumerable<ProposalResponse>>> Handle(GetProposalsByTenantQuery request, CancellationToken cancellationToken)
        {
            var proposals = await unitOfWork.Proposals.GetAsync(p => p.TenantId == TenantId.Of(currentUserService.TenantId!.Value), includeString: "Items");

            return proposals.ToDto();
        }
    }
}

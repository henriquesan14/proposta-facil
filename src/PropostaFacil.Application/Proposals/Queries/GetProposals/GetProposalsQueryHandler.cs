using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;
using System.Linq.Expressions;

namespace PropostaFacil.Application.Proposals.Queries.GetProposals
{
    public class GetProposalsQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IQueryHandler<GetProposalsQuery, ResultT<PaginatedResult<ProposalResponse>>>
    {
        public async Task<ResultT<PaginatedResult<ProposalResponse>>> Handle(GetProposalsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Proposal> proposals;
            int count;
            List<Expression<Func<Proposal, object>>> includes = new List<Expression<Func<Proposal, object>>>()
            {
                p => p.Items
            };

            if (currentUserService.Role == UserRoleEnum.AdminSystem)
            {
                proposals = await unitOfWork.Proposals.GetAsync(
                    pageNumber: request.PaginationRequest.PageIndex,
                    pageSize: request.PaginationRequest.PageSize,
                    includes: includes
                );

                count = await unitOfWork.Proposals.GetCountAsync();
            }
            else
            {
                var tenantId = TenantId.Of(currentUserService.TenantId!.Value);

                proposals = await unitOfWork.Proposals.GetAsync(
                    u => u.TenantId == tenantId,
                    pageNumber: request.PaginationRequest.PageIndex,
                    pageSize: request.PaginationRequest.PageSize,
                    includes: includes
                );

                count = await unitOfWork.Proposals.GetCountAsync(u => u.TenantId == tenantId);
            }

            var dto = proposals.ToDto();

            var paginated = new PaginatedResult<ProposalResponse>(
                request.PaginationRequest.PageIndex,
                request.PaginationRequest.PageSize,
                count,
                dto
            );

            return paginated;
        }
    }
}

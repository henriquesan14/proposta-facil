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

                Expression<Func<Proposal, bool>> predicate = p =>
                 (!request.ClientId.HasValue || p.ClientId == ClientId.Of(request.ClientId.Value)) &&
                 (!request.TenantId.HasValue || p.TenantId == TenantId.Of(request.TenantId.Value)) &&
                 (string.IsNullOrEmpty(request.Number) ||
                    p.Number == request.Number) &&
                (string.IsNullOrEmpty(request.Title) ||
                    p.Title.ToLower().Contains(request.Title.ToLower())) &&
                (!request.ProposalStatus.HasValue || p.ProposalStatus == request.ProposalStatus);

                proposals = await unitOfWork.Proposals.GetAsync(
                    predicate: predicate,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    includes: includes
                );

                count = await unitOfWork.Proposals.GetCountAsync(predicate);
            }
            else
            {
                var tenantId = TenantId.Of(currentUserService.TenantId!.Value);
                Expression<Func<Proposal, bool>> predicate = p =>
                 (p.TenantId == tenantId) &&
                 (!request.ClientId.HasValue || p.ClientId == ClientId.Of(request.ClientId.Value)) &&
                 (string.IsNullOrEmpty(request.Number) ||
                    p.Number == request.Number) &&
                (string.IsNullOrEmpty(request.Title) ||
                    p.Title.ToLower().Contains(request.Title.ToLower())) &&
                (!request.ProposalStatus.HasValue || p.ProposalStatus == request.ProposalStatus);

                proposals = await unitOfWork.Proposals.GetAsync(
                    predicate: predicate,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    includes: includes
                );

                count = await unitOfWork.Proposals.GetCountAsync(predicate);
            }

            var dto = proposals.ToDto();

            var paginated = new PaginatedResult<ProposalResponse>(
                request.PageNumber,
                request.PageSize,
                count,
                dto
            );

            return paginated;
        }
    }
}

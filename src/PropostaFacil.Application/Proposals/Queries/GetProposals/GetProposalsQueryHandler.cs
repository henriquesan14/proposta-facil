using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Proposals.Specifications;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Proposals.Queries.GetProposals;

public class GetProposalsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetProposalsQuery, ResultT<PaginatedResult<ProposalResponse>>>
{
    public async Task<ResultT<PaginatedResult<ProposalResponse>>> Handle(GetProposalsQuery request, CancellationToken cancellationToken)
    {
        var spec = new ListProposalsSpecification(request.DocumentClient, request.Number, request.Title, request.ProposalStatus, request.OnlyActive);
        var paginated = await unitOfWork.Proposals.ToPaginatedListAsync(
            spec,
            request.PageIndex,
            request.PageSize,
            p => p.ToDto()
        );

        return paginated;
    }
}

using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Proposals.Queries.GetProposals
{
    public record GetProposalsQuery(PaginationRequest PaginationRequest) : IQuery<ResultT<PaginatedResult<ProposalResponse>>>;
}

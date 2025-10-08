using Common.ResultPattern;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Proposals.Queries.GetProposals;

public record GetProposalsQuery(string? DocumentClient, string? Number,
    string? Title, ProposalStatusEnum? ProposalStatus, bool OnlyActive = true, int PageIndex = 1, int PageSize = 20) : IQuery<ResultT<PaginatedResult<ProposalResponse>>>;

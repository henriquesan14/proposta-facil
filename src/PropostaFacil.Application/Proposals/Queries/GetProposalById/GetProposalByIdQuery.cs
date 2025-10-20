using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Queries.GetProposalById;

public record GetProposalByIdQuery(Guid Id) : IQuery<ResultT<ProposalResponse>>;

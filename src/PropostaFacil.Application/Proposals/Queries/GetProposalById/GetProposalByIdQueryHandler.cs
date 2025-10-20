using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Proposals.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Queries.GetProposalById;

public class GetProposalByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetProposalByIdQuery, ResultT<ProposalResponse>>
{
    public async Task<ResultT<ProposalResponse>> Handle(GetProposalByIdQuery request, CancellationToken cancellationToken)
    {
        var proposal = await unitOfWork.Proposals.SingleOrDefaultAsync(new GetProposalByIdSpecification(ProposalId.Of(request.Id)));
        if (proposal == null) return ProposalErrors.NotFound(request.Id);

        return proposal.ToDto();
    }
}

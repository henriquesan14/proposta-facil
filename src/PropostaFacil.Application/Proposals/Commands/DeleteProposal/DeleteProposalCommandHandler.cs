using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Proposals.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.DeleteProposal;

public class DeleteProposalCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteProposalCommand, Result>
{
    public async Task<Result> Handle(DeleteProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = await unitOfWork.Proposals.SingleOrDefaultAsync(new GetProposalByIdSpecification(ProposalId.Of(request.Id)));
        if (proposal == null) return ProposalErrors.NotFound(request.Id);

        await unitOfWork.BeginTransaction();

        unitOfWork.Proposals.SoftDelete(proposal);
        await unitOfWork.Proposals.SoftDeleteProposalItems(proposal.Id);


        await unitOfWork.CompleteAsync();
        await unitOfWork.CommitAsync();

        return Result.Success();
    }
}

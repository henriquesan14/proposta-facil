using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Proposals.Specifications;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.SendProposal;

public class SendProposalCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<SendProposalCommand, Result>
{
    public async Task<Result> Handle(SendProposalCommand request, CancellationToken cancellationToken)
    {
        var subscriptionActive = await unitOfWork.Subscriptions.SingleOrDefaultAsync(new GetSubscriptionByStatusSpecification(SubscriptionStatusEnum.Active));

        if (subscriptionActive is null) return SubscriptionErrors.InactiveSubscription();
        if (subscriptionActive.ProposalsUsed >= subscriptionActive.SubscriptionPlan.MaxProposalsPerMonth) return SubscriptionErrors.SubscriptionLimit();

        var proposal = await unitOfWork.Proposals.SingleOrDefaultAsync(new GetProposalByIdSpecification(ProposalId.Of(request.ProposalId)));
        if (proposal == null) return ProposalErrors.NotFound(request.ProposalId);

        if(proposal.ProposalStatus != Domain.Enums.ProposalStatusEnum.Draft) return SubscriptionErrors.ProposalAlreadySent();

        proposal.SendProposal();
        subscriptionActive.IncrementProposalsUsed();

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}

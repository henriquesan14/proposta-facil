using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using System.Linq.Expressions;

namespace PropostaFacil.Application.Proposals.Commands.SendProposal
{
    public class SendProposalCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<SendProposalCommand, Result>
    {
        public async Task<Result> Handle(SendProposalCommand request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Subscription, object>>> includesSubscription = new List<Expression<Func<Subscription, object>>>()
            {
                s => s.SubscriptionPlan
            };

            List<Expression<Func<Proposal, object>>> includesProposal = new List<Expression<Func<Proposal, object>>>()
            {
                p => p.Client,
                p => p.Items
            };

            Expression<Func<Subscription, bool>>  predicate = s => s.TenantId == TenantId.Of(currentUserService.TenantId!.Value) &&
            s.Status == Domain.Enums.SubscriptionStatusEnum.Active;

            var subscriptionActive = await unitOfWork.Subscriptions.GetSingleAsync(predicate, includes: includesSubscription);

            if (subscriptionActive is null) return SubscriptionErrors.InactiveSubscription();
            if (subscriptionActive.ProposalsUsed >= subscriptionActive.SubscriptionPlan.MaxProposalsPerMonth) return SubscriptionErrors.SubscriptionLimit();

            var proposal = await unitOfWork.Proposals.GetByIdAsync(ProposalId.Of(request.ProposalId), includes: includesProposal);
            if (proposal == null) return ProposalErrors.NotFound(request.ProposalId);

            if(proposal.ProposalStatus != Domain.Enums.ProposalStatusEnum.Draft) return SubscriptionErrors.ProposalAlreadySent();

            proposal.SendProposal();
            subscriptionActive.IncrementProposalsUsed();

            await unitOfWork.CompleteAsync();

            return Result.Success();
        }
    }
}

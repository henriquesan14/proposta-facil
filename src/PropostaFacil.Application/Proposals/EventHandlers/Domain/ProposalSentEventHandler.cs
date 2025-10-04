using MassTransit;
using MediatR;
using PropostaFacil.Domain.Events;
using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Application.Proposals.EventHandlers.Domain;

public class ProposalSentEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<ProposalSentEvent>
{
    public async Task Handle(ProposalSentEvent proposalSentEvent, CancellationToken cancellationToken)
    {
        var proposalSentIntegrationEvent = new ProposalSentIntegrationEvent(
            proposalSentEvent.Proposal.Id.Value,
            proposalSentEvent.Proposal.TenantId.Value,
            proposalSentEvent.Proposal.ClientId.Value,
            proposalSentEvent.Proposal.Number,
            proposalSentEvent.Proposal.Title,
            proposalSentEvent.Proposal.TotalAmount.Amount,
            proposalSentEvent.Proposal.TotalAmount.Currency,
            proposalSentEvent.Proposal.Client.Contact.Email,
            proposalSentEvent.Proposal.Client.Name,
            proposalSentEvent.Proposal.ValidUntil,
            proposalSentEvent.Proposal.Items.Select(i => new ProposalItemIntegrationEvent(i.Name, i.Description, i.Quantity, i.UnitPrice, i.TotalPrice))
        );
        await publishEndpoint.Publish(proposalSentIntegrationEvent, cancellationToken);
    }
}

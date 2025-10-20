using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Proposals;
using PropostaFacil.Domain.Proposals.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.UpdateProposal;

public class UpdateProposalCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateProposalCommand, Result>
{
    public async Task<Result> Handle(UpdateProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = await unitOfWork.Proposals.SingleOrDefaultAsync(new GetProposalByIdSpecification(ProposalId.Of(request.Id)));
        if (proposal == null) return ProposalErrors.NotFound(request.Id);

        proposal.Update(
            ClientId.Of(request.ClientId),
            request.Title,
            request.Currency,
            request.ValidUntil
        );

        var requestItems = request.Items.ToList();

        // Atualizar existentes
        foreach (var existing in proposal.Items.ToList())
        {
            var match = requestItems.FirstOrDefault(i => i.Id.HasValue && ProposalItemId.Of(i.Id.Value) == existing.Id);

            if (match != null)
            {
                existing.Update(match.Name, match.Description, match.Quantity, match.UnitPrice);
            }
            else
            {
                // Remover se não existe mais
                proposal.RemoveItem(existing.Id.Value);
            }
        }

        // Adicionar novos
        var existingIds = proposal.Items.Select(i => i.Id.Value).ToHashSet();
        foreach (var newItem in requestItems.Where(i => !i.Id.HasValue || i.Id == Guid.Empty))
        {
            var item = ProposalItem.Create(newItem.Name, newItem.Description, newItem.Quantity, newItem.UnitPrice);
            proposal.AddItem(item);
        }

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}

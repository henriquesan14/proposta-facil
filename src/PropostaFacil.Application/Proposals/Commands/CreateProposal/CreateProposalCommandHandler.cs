using Common.ResultPattern;
using PropostaFacil.Application.Contracts.Data;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.CreateProposal
{
    public class CreateProposalCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateProposalCommand, ResultT<ProposalResponse>>
    {
        public async Task<ResultT<ProposalResponse>> Handle(CreateProposalCommand request, CancellationToken cancellationToken)
        {
            var proposal = Proposal.Create(TenantId.Of(request.TenantId), ClientId.Of(request.ClientId), request.Number, request.Title, request.ProposalStatus,
                request.Currency, request.ValidUntil);

            foreach (var item in request.Items)
            {
                proposal.AddItem(ProposalItem.Create(item.Name, item.Description, item.Quantity, item.UnitPrice));
            }

            await unitOfWork.Proposals.AddAsync(proposal);
            await unitOfWork.CompleteAsync();

            return proposal.ToDto();
        }
    }
}

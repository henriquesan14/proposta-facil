using Common.ResultPattern;
using PropostaFacil.Application.Clients;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.CreateProposal
{
    public class CreateProposalCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<CreateProposalCommand, ResultT<ProposalResponse>>
    {
        public async Task<ResultT<ProposalResponse>> Handle(CreateProposalCommand request, CancellationToken cancellationToken)
        {
            var loggedTenantId = TenantId.Of(currentUserService.TenantId!.Value);

            var clientExist = await unitOfWork.Clients.GetByIdAsync(ClientId.Of(request.ClientId));

            if (clientExist is null) return ClientErrors.NotFound(request.ClientId);

            var proposal = Proposal.Create(loggedTenantId, ClientId.Of(request.ClientId), request.Title, request.ProposalStatus,
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

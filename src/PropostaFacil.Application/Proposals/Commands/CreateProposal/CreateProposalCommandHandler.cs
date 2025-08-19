using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Proposals.Commands.CreateProposal
{
    public class CreateProposalCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<CreateProposalCommand, ResultT<ProposalResponse>>
    {
        public async Task<ResultT<ProposalResponse>> Handle(CreateProposalCommand request, CancellationToken cancellationToken)
        {
            var loggedRole = currentUserService.Role;
            var loggedTenantId = currentUserService.TenantId;

            Guid tenantIdToUse;

            if (loggedRole == UserRoleEnum.AdminSystem)
            {
                if (request.TenantId is null)
                    return TenantErrors.TenantRequired();

                tenantIdToUse = request.TenantId.Value;
                var tenantExist = await unitOfWork.Tenants.GetByIdAsync(TenantId.Of(tenantIdToUse));
                if (tenantExist is null)
                    return TenantErrors.NotFound(tenantIdToUse);
            }
            else
            {
                tenantIdToUse = loggedTenantId!.Value;
            }

            var clientBelongsToTenant = await unitOfWork.Clients.GetSingleAsync(c => c.TenantId == TenantId.Of(tenantIdToUse) && c.Id == ClientId.Of(request.ClientId));

            if (clientBelongsToTenant is null) return ProposalErrors.Forbidden(request.ClientId);

            var proposal = Proposal.Create(TenantId.Of(tenantIdToUse), ClientId.Of(request.ClientId), request.Title, request.ProposalStatus,
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

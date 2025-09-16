using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Domain.Clients;
using PropostaFacil.Domain.Clients.Specifications;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<CreateClientCommand, ResultT<ClientResponse>>
    {
        public async Task<ResultT<ClientResponse>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
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

            var clientExist = await unitOfWork.Clients.FirstOrDefaultAsync(new GetClientByDocumentGlobalSpecification(request.Document));

            if (clientExist != null) return ClientErrors.Conflict(request.Document);

            var document = Document.Of(request.Document);
            var contact = Contact.Of(request.Email, request.PhoneNumber);
            var address = Address.Of(request.AddressStreet, request.AddressNumber, request.AddressComplement, request.AddressDistrict,
                request.AddressCity, request.AddressState, request.AddressZipCode);
            var client = Client.Create(request.Name, TenantId.Of(tenantIdToUse), document, contact, address);
            await unitOfWork.Clients.AddAsync(client);

            await unitOfWork.CompleteAsync();

            return client.ToDto();
        }
    }
}

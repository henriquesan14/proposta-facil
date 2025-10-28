using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Clients;
using PropostaFacil.Domain.Clients.Contracts;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Commands.CreateClient;

public class CreateClientCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, IClientRuleCheck clientDocumentCheck) : ICommandHandler<CreateClientCommand, ResultT<ClientResponse>>
{
    public async Task<ResultT<ClientResponse>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var loggedTenantId = userContext.TenantId;

        var document = Document.Of(request.Document);
        var contact = Contact.Of(request.Email, request.PhoneNumber);
        var address = Address.Of(request.AddressStreet, request.AddressNumber, request.AddressComplement, request.AddressDistrict,
            request.AddressCity, request.AddressState, request.AddressZipCode);
        var client = Client.Create(request.Name, TenantId.Of(loggedTenantId!.Value), document, contact, address, clientDocumentCheck);
        await unitOfWork.Clients.AddAsync(client);

        await unitOfWork.CompleteAsync();

        return client.ToDto();
    }
}

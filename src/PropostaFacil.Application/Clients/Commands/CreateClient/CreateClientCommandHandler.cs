using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateClientCommand, ResultT<ClientResponse>>
    {
        public async Task<ResultT<ClientResponse>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var clientExist = await unitOfWork.Clients.GetSingleAsync(t => t.Document.Number == request.Document);

            if (clientExist != null) return ClientErrors.Conflict(request.Document);

            var document = Document.Of(request.Document);
            var contact = Contact.Of(request.Email, request.PhoneNumber);
            var address = Address.Of(request.AddressStreet, request.AddressNumber, request.AddressComplement, request.AddressDistrict,
                request.AddressCity, request.AddressState, request.AddressZipCode);
            var client = Client.Create(request.Name, TenantId.Of(request.TenantId), document, contact, address);
            await unitOfWork.Clients.AddAsync(client);

            await unitOfWork.CompleteAsync();

            return client.ToDto();
        }
    }
}

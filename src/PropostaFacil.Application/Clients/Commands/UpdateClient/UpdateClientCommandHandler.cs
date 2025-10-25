using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Clients.Specifications;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateClientCommand, Result>
{
    public async Task<Result> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var clientId = ClientId.Of(request.Id);
        var client = await unitOfWork.Clients.SingleOrDefaultAsync(new GetClientByIdSpecification(clientId));
        if (client == null) return ClientErrors.NotFound(request.Id);

        var clientExist = await unitOfWork.Clients.SingleOrDefaultAsync(new GetClientByDocumentSpecification(request.Document));

        if (clientExist != null && clientExist.Id != clientId) return ClientErrors.Conflict(request.Document);

        var document = Document.Of(request.Document);
        var contact = Contact.Of(request.Email, request.PhoneNumber);
        var address = Address.Of(request.AddressStreet, request.AddressNumber, request.AddressComplement, request.AddressDistrict,
            request.AddressCity, request.AddressState, request.AddressZipCode);
        client.Update(request.Name, document, contact, address);

        await unitOfWork.CompleteAsync();


        return Result.Success();
    }
}

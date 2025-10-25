using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Commands.UpdateClient;

public record UpdateClientCommand(Guid Id, string Name, string Document, string Email, string PhoneNumber,
    string AddressStreet, string AddressNumber, string AddressComplement, string AddressDistrict, string AddressCity,
    string AddressState, string AddressZipCode) : ICommand<Result>;


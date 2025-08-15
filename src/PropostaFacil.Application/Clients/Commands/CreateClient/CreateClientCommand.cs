using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Commands.CreateClient
{
    public record CreateClientCommand(string Name, string Document, string Email, string PhoneNumber, string AddressStreet,
        string AddressNumber, string AddressComplement, string AddressDistrict, string AddressCity,
        string AddressState, string AddressZipCode) : ICommand<ResultT<ClientResponse>>;
}

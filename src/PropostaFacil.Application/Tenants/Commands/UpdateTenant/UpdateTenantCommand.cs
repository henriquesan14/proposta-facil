using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.UpdateTenant
{
    public record UpdateTenantCommand(Guid Id, string Name, string Domain, string Document, string Email, string PhoneNumber,
        string AddressStreet, string AddressNumber, string AddressComplement, string AddressDistrict, string AddressCity,
        string AddressState, string AddressZipCode) : ICommand<Result>;
}

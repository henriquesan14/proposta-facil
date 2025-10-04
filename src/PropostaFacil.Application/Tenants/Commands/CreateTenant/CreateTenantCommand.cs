using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.CreateTenant;

public record CreateTenantCommand(string Name, string Domain, string Document, string Email, string PhoneNumber,
    string AddressStreet, string AddressNumber, string AddressComplement, string AddressDistrict, string AddressCity,
    string AddressState, string AddressZipCode) : ICommand<ResultT<TenantResponse>>;

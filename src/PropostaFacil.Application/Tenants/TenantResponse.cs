namespace PropostaFacil.Application.Tenants
{
    public record TenantResponse(Guid Id, string Name, string Domain, string Document, string Email, string PhoneNumber,
        string AddressStreet, string AddressNumber, string AddressComplement, string AddressDistrict, string AddressCity,
        string AddressState, string AddressZipCode);
}

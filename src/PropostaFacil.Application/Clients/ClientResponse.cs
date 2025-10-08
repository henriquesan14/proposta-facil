namespace PropostaFacil.Application.Clients;

public record ClientResponse(Guid Id, string Name, Guid TenantId, string Document, string Email, string PhoneNumber, string AddressStreet, string AddressNumber, string? AddressComplement, string AddressDistrict, string AddressCity,
    string AddressState, string AddressZipCode, bool IsActive, DateTime? CreatedAt);

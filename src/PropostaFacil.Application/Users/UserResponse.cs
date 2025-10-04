using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Application.Users;

public record UserResponse(Guid Id, string Name, string Email, string PhoneNumber, UserRoleEnum Role, Guid? TenantId);

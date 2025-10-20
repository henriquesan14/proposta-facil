using PropostaFacil.Application.Tenants;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Application.Auth;

public record AuthResponse(Guid UserId, string Name, UserRoleEnum Role, TenantResponse? TenantImpersonate = null);

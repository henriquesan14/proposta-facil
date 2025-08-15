using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Application.Auth
{
    public record AuthResponse(Guid UserId, string Name, UserRoleEnum Role, Guid TenantId);
}

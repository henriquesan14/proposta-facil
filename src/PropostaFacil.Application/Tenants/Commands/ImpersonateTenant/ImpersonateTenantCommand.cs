using Common.ResultPattern;
using PropostaFacil.Application.Auth;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.ImpersonateTenant;

public record ImpersonateTenantCommand(Guid TenantId) : ICommand<ResultT<AuthResponse>>;


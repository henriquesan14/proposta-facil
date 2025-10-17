using Common.ResultPattern;
using PropostaFacil.Application.Auth;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.StopImpersonateTenant;

public record StopImpersonateTenantCommand : ICommand<ResultT<AuthResponse>>;

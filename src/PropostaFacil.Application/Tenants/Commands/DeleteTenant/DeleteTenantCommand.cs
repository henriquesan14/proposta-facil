using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.DeleteTenant;

public record DeleteTenantCommand(Guid Id) : ICommand<Result>;

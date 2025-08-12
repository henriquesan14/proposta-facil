using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.UpdateTenant
{
    public record UpdateTenantCommand(Guid Id, string Name, string Cnpj, string Domain) : ICommand<Result>;
}

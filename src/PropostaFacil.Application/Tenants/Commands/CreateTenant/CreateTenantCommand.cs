using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.CreateTenant
{
    public record CreateTenantCommand(string Name, string Cnpj, string Domain) : ICommand<ResultT<TenantResponse>>;
}

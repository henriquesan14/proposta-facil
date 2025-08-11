using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Queries.GetTenants
{
    public record GetTenantsQuery : IQuery<ResultT<IEnumerable<TenantResponse>>>;
}

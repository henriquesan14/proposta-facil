using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Companies.Queries.GetTenants
{
    public record GetTenantsQuery : IQuery<ResultT<IEnumerable<TenantResponse>>>;
}

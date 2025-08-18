using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Tenants.Queries.GetTenants
{
    public record GetTenantsQuery(string Name, string Document, int PageNumber = 1, int PageSize = 20) : IQuery<ResultT<PaginatedResult<TenantResponse>>>;
}

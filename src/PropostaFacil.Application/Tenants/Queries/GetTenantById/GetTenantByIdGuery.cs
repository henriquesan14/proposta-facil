using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Queries.GetTenantById
{
    public record GetTenantByIdGuery(Guid Id) : IQuery<ResultT<TenantResponse>>;
}

using Common.ResultPattern;
using PropostaFacil.Application.Contracts.Data;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Companies.Queries.GetTenants
{
    public class GetTenantsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetTenantsQuery, ResultT<IEnumerable<TenantResponse>>>
    {
        public async Task<ResultT<IEnumerable<TenantResponse>>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
        {
            var tenants = await unitOfWork.Tenants.GetAllAsync();

            return tenants.ToDto();
        }
    }
}

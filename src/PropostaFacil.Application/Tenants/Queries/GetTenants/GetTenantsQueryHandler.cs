using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;
using System.Linq.Expressions;

namespace PropostaFacil.Application.Tenants.Queries.GetTenants
{
    public class GetTenantsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetTenantsQuery, ResultT<PaginatedResult<TenantResponse>>>
    {
        public async Task<ResultT<PaginatedResult<TenantResponse>>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Tenant, bool>> predicate = p =>
                (string.IsNullOrEmpty(request.Name) ||
                    p.Name.ToLower().Contains(request.Name.ToLower())) &&
                (string.IsNullOrEmpty(request.Document) ||
                    p.Document.Number == request.Document);
            var tenants = await unitOfWork.Tenants.GetAsync(predicate);
            var count = await unitOfWork.Tenants.GetCountAsync(predicate);

            var dto = tenants.ToDto();

            var paginated = new PaginatedResult<TenantResponse>(
                request.PageNumber,
                request.PageSize,
                count,
                dto
            );

            return paginated;
        }
    }
}

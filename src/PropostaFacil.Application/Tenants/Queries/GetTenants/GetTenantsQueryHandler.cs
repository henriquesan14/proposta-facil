using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Tenants.Specifications;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Tenants.Queries.GetTenants;

public class GetTenantsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetTenantsQuery, ResultT<PaginatedResult<TenantResponse>>>
{
    public async Task<ResultT<PaginatedResult<TenantResponse>>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
    {
        var spec = new ListTenantsGlobalSpecification(request.Name, request.Document, request.OnlyActive);
        var paginated = await unitOfWork.Tenants.ToPaginatedListAsync(spec, request.PageIndex, request.PageSize, t => t.ToDto());

        return paginated;
    }
}

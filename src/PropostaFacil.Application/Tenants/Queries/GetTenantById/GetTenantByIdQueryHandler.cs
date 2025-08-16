using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Queries.GetTenantById
{
    public class GetTenantByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetTenantByIdGuery, ResultT<TenantResponse>>
    {
        public async Task<ResultT<TenantResponse>> Handle(GetTenantByIdGuery request, CancellationToken cancellationToken)
        {
            var tenant = await unitOfWork.Tenants.GetByIdAsync(TenantId.Of(request.Id));
            if (tenant == null) return TenantErrors.NotFound(request.Id);

            return tenant.ToDto();
        }
    }
}

using Common.ResultPattern;
using PropostaFacil.Application.Contracts.Data;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Companies.Commands.CreateTenant
{
    public class CreateTenantCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateTenantCommand, ResultT<TenantResponse>>
    {
        public async Task<ResultT<TenantResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenantExist = await unitOfWork.Tenants.GetSingleAsync(t => t.Name == request.Name);

            if (tenantExist != null) return TenantErrors.Conflict(request.Name);

            var tenant = Tenant.Create(request.Name, request.Cnpj, request.Domain);
            await unitOfWork.Tenants.AddAsync(tenant);

            await unitOfWork.CompleteAsync();

            return tenant.ToDto();
        }
    }
}

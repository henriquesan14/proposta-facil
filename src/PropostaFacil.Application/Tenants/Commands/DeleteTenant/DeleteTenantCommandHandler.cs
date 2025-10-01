using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Tenants.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.DeleteTenant;

public class DeleteTenantCommandHandler(IUnitOfWork unitOfWork, IAsaasService asaasService) : ICommandHandler<DeleteTenantCommand, Result>
{
    public async Task<Result> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await unitOfWork.Tenants.SingleOrDefaultAsync(new GetTenantByIdGlobalSpecification(TenantId.Of(request.Id)));
        if (tenant == null) return TenantErrors.NotFound(request.Id);

        await unitOfWork.BeginTransaction();

        unitOfWork.Tenants.Remove(tenant);

        await asaasService.DeleteCustomer(tenant.AsaasId);

        await unitOfWork.CompleteAsync();
        await unitOfWork.CommitAsync();

        return Result.Success();
    }
}

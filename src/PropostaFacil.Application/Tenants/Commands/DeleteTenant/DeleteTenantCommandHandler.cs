using Common.ResultPattern;
using PropostaFacil.Application.Contracts.Data;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.DeleteTenant
{
    public class DeleteTenantCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteTenantCommand, Result>
    {
        public async Task<Result> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await unitOfWork.Tenants.GetByIdAsync(TenantId.Of(request.Id));
            if (tenant == null) return TenantErrors.NotFound(request.Id);

            unitOfWork.Tenants.Remove(tenant);
            await unitOfWork.CompleteAsync();

            return Result.Success();
        }
    }
}

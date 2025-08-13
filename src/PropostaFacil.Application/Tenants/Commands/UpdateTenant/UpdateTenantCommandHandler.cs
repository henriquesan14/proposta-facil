using Common.ResultPattern;
using PropostaFacil.Application.Contracts.Data;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.UpdateTenant
{
    public class UpdateTenantCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateTenantCommand, Result>
    {
        public async Task<Result> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await unitOfWork.Tenants.GetByIdAsync(TenantId.Of(request.Id));
            if (tenant == null) return TenantErrors.NotFound(request.Id);

            if (!request.Name.Equals(tenant.Name, StringComparison.OrdinalIgnoreCase))
            {
                var tenantExist = await unitOfWork.Tenants.GetSingleAsync(t => t.Name.Equals(request.Name));
                if (tenantExist != null && tenantExist.Id != TenantId.Of(request.Id)) return TenantErrors.Conflict(request.Name);
            }

            var document = Document.Of(request.Document);
            var contact = Contact.Of(request.Email, request.PhoneNumber);
            var address = Address.Of(request.AddressStreet, request.AddressNumber, request.AddressComplement, request.AddressDistrict,
                request.AddressCity, request.AddressState, request.AddressZipCode);
            tenant.Update(request.Name, request.Domain, document, contact, address);

            await unitOfWork.CompleteAsync();

            return Result.Success();
        }
    }
}

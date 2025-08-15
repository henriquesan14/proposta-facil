using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateTenantCommand, ResultT<TenantResponse>>
    {
        public async Task<ResultT<TenantResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenantExist = await unitOfWork.Tenants.GetSingleAsync(t => t.Document.Number == request.Document);

            if (tenantExist != null) return ClientErrors.Conflict(request.Document);

            var document = Document.Of(request.Document);
            var contact = Contact.Of(request.Email, request.PhoneNumber);
            var address = Address.Of(request.AddressStreet, request.AddressNumber, request.AddressComplement, request.AddressDistrict,
                request.AddressCity, request.AddressState, request.AddressZipCode);
            var tenant = Tenant.Create(request.Name, request.Domain, document, contact, address);
            await unitOfWork.Tenants.AddAsync(tenant);

            await unitOfWork.CompleteAsync();

            return tenant.ToDto();
        }
    }
}

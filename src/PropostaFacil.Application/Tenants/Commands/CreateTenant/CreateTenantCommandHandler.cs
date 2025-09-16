using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Shared.Request;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.Tenants.Specifications;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler(IUnitOfWork unitOfWork, IAsaasService asaasService) : ICommandHandler<CreateTenantCommand, ResultT<TenantResponse>>
    {
        public async Task<ResultT<TenantResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenantExist = await unitOfWork.Tenants.SingleOrDefaultAsync(new GetTenantByDocumentGlobalSpecification(request.Document));

            if (tenantExist != null) return TenantErrors.Conflict(request.Document);

            var document = Document.Of(request.Document);
            var contact = Contact.Of(request.Email, request.PhoneNumber);
            var address = Address.Of(request.AddressStreet, request.AddressNumber, request.AddressComplement, request.AddressDistrict,
                request.AddressCity, request.AddressState, request.AddressZipCode);

            string customerId = await asaasService.GetCustomerIdByCpfCnpj(request.Document);
            if(customerId is null)
            {
                var customerAsaas = new CustomerAsaasRequest(request.Document, request.Name, request.Email, request.PhoneNumber, true);
                customerId = await asaasService.CreateCustomer(customerAsaas);
            }

            await unitOfWork.BeginTransaction();

            var tenant = Tenant.Create(request.Name, request.Domain, document, contact, address, customerId);
            await unitOfWork.Tenants.AddAsync(tenant);

            await unitOfWork.CompleteAsync();
            await unitOfWork.CommitAsync();

            return tenant.ToDto();
        }
    }
}

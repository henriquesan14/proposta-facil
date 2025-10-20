using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Shared.Request;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.Tenants.Contracts;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.CreateTenant;

public class CreateTenantCommandHandler(IUnitOfWork unitOfWork, IAsaasService asaasService, ITenantRuleCheck tenantRuleCheck) : ICommandHandler<CreateTenantCommand, ResultT<TenantResponse>>
{
    public async Task<ResultT<TenantResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
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

        try
        {
            var tenant = Tenant.Create(request.Name, request.Domain, document, contact, address, customerId, tenantRuleCheck);
            await unitOfWork.Tenants.AddAsync(tenant);

            await unitOfWork.CompleteAsync();

            return tenant.ToDto();
        }
        catch (Exception) {
            await asaasService.DeleteCustomer(customerId);
            throw;
        }
    }
}

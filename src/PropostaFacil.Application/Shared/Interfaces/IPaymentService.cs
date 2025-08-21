using PropostaFacil.Application.Shared.Request;
using PropostaFacil.Application.Shared.Response;

namespace PropostaFacil.Application.Shared.Interfaces
{
    public interface IPaymentService
    {
        Task<GenerateChargeResponse> GenerateCharge(ChargeAsaasRequest request);
        Task<AsaasResponse<PaymentResponse>> GetChargesByCustomerId(string customerId, int offset, int limit);
        Task<DeleteChargeResponse> DeleteCharge(string chargeId);
        Task<string> GetCustomerIdByCpfCnpj(string cpfCnpj);
        Task<string> CreateCustomer(CustomerAsaasRequest request);
    }
}

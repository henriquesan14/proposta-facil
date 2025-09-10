using PropostaFacil.Application.Shared.Request;
using PropostaFacil.Application.Shared.Response;

namespace PropostaFacil.Application.Shared.Interfaces
{
    public interface IAsaasService
    {
        Task<AsaasResponse<PaymentResponse>> GetPaymentsBySubscription(string subscriptionId);
        Task<GenerateChargeResponse> GenerateCharge(ChargeAsaasRequest request);
        Task<SubscriptionAsaasResponse> CreateSubscriptionAsync(CreateSubscriptionRequest request);
        Task<AsaasResponse<PaymentResponse>> GetChargesByCustomerId(string customerId, int offset, int limit);
        Task<DeleteChargeResponse> DeleteCharge(string chargeId);
        Task<string> GetCustomerIdByCpfCnpj(string cpfCnpj);
        Task<string> CreateCustomer(CustomerAsaasRequest request);
        Task<PaymentLinkAsaasResponse> CreatePaymentLink(PaymentLinkAsaasRequest request);
    }
}

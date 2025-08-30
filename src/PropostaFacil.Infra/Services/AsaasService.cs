using Microsoft.Extensions.Configuration;
using PropostaFacil.Application.Shared.Exceptions;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Shared.Request;
using PropostaFacil.Application.Shared.Response;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace PropostaFacil.Infra.Services
{
    public class AsaasService : IAsaasService
    {
        private readonly IConfiguration _configuration;
        private readonly string _accessToken;
        private readonly string _baseUrl;

        public AsaasService(IConfiguration configuration)
        {
            _configuration = configuration;
            _accessToken = _configuration["AsaasSettings:AccessToken"]!; // Token de acesso do Asaas
            _baseUrl = _configuration["AsaasSettings:BaseUrl"]!;
        }

        public async Task<AsaasResponse<PaymentResponse>> GetPaymentsBySubscription(string subscriptionId)
        {
            return await SendRequestAsync<AsaasResponse<PaymentResponse>>($"subscriptions/{subscriptionId}/payments", Method.Get);
        }

        public async Task<GenerateChargeResponse> GenerateCharge(ChargeAsaasRequest request)
        {
            var response = await SendRequestAsync<GenerateChargeResponse>("payments", Method.Post, request);
            return response;
        }

        public async Task<SubscriptionAsaasResponse> CreateSubscriptionAsync(CreateSubscriptionRequest request)
        {
            return await SendRequestAsync<SubscriptionAsaasResponse>("subscriptions", Method.Post, request);
        }

        public async Task<DeleteChargeResponse> DeleteCharge(string chargeId)
        {
            var response = await SendRequestAsync<DeleteChargeResponse>($"payments/{chargeId}", Method.Delete);
            return response;
        }

        public async Task<AsaasResponse<PaymentResponse>> GetChargesByCustomerId(string customerId, int offset, int limit)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "customer", customerId },
                { "offset", offset.ToString() },
                { "limit", limit.ToString() }
            };

            return await SendRequestAsync<AsaasResponse<PaymentResponse>>("payments", Method.Get, queryParams: queryParams);
        }

        public async Task<string> GetCustomerIdByCpfCnpj(string cpfCnpj)
        {
            var queryParams = new Dictionary<string, string>
        {
            { "cpfCnpj", cpfCnpj }
        };

            var result = await SendRequestAsync<AsaasResponse<CustomerResponse>>("customers", Method.Get, queryParams: queryParams);
            return result.Data.FirstOrDefault()?.Id;
        }

        public async Task<string> CreateCustomer(CustomerAsaasRequest request)
        {
            var result = await SendRequestAsync<CustomerResponse>("customers", Method.Post, request);
            return result.Id;
        }

        public async Task<PaymentLinkAsaasResponse> CreatePaymentLink(PaymentLinkAsaasRequest request)
        {
            var result = await SendRequestAsync<PaymentLinkAsaasResponse>("paymentLinks", Method.Post, request);
            return result;
        }

        private async Task<T> SendRequestAsync<T>(string resource, Method method, object? body = null, Dictionary<string, string>? queryParams = null)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest(resource, method);

            request.AddHeader("access_token", _accessToken);
            request.AddHeader("Content-Type", "application/json");

            if (body is not null)
                request.AddJsonBody(body);

            if (queryParams is not null)
            {
                foreach (var (key, value) in queryParams)
                    request.AddQueryParameter(key, value);
            }

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return JsonSerializer.Deserialize<T>(response.Content!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            else
            {
                var message = TryExtractErrorMessage(response.Content) ?? "Erro ao processar requisição com o Asaas.";
                throw new IntegrationException(message, (HttpStatusCode?)response.StatusCode ?? HttpStatusCode.BadRequest);
            }
        }

        private string? TryExtractErrorMessage(string? content)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(content))
                    return null;

                using var jsonDoc = JsonDocument.Parse(content);
                if (jsonDoc.RootElement.TryGetProperty("errors", out var errorsElement))
                {
                    var firstError = errorsElement.EnumerateArray().FirstOrDefault();
                    if (firstError.TryGetProperty("description", out var desc))
                        return desc.GetString();
                }
            }
            catch
            {
                // Ignora erros ao tentar extrair mensagens
            }

            return null;
        }
    }
}

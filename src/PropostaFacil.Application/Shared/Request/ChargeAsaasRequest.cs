using PropostaFacil.Domain.Enums;
using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Request;

public record ChargeAsaasRequest(
    [property: JsonPropertyName("customer")] string Customer,
    [property: JsonPropertyName("billingType")][property: JsonConverter(typeof(JsonStringEnumConverter))] BillingTypeEnum BillingType,
    [property: JsonPropertyName("value")] decimal Value,
    [property: JsonPropertyName("totalValue")] decimal TotalValue,
    [property: JsonPropertyName("dueDate")] DateTime DueDate,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("installmentCount")] int InstallmentCount,
    [property: JsonPropertyName("callback")][property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] CallbackAsaas? Callback);

public record CallbackAsaas([property: JsonPropertyName("successUrl")] string SuccessUrl);

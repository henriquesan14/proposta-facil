using PropostaFacil.Domain.Enums;
using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Request;

public record UpdateSubscriptionRequest(
[property: JsonPropertyName("billingType")][property: JsonConverter(typeof(JsonStringEnumConverter))] BillingTypeEnum BillingType,
[property: JsonPropertyName("value")] decimal Value,
[property: JsonPropertyName("description")] string? Description,
[property: JsonPropertyName("updatePendingPayments")] bool updatePendingPayments = true
);


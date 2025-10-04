using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Request;

public record PaymentLinkAsaasRequest(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("value")] decimal Value,
    [property: JsonPropertyName("billingType")] string BillingType,
    [property: JsonPropertyName("chargeType")] string ChargeType,
    [property: JsonPropertyName("subscriptionCycle")] string SubscriptionCycle,
    [property: JsonPropertyName("dueDateLimitDays")] int DueDateLimitDays
    );

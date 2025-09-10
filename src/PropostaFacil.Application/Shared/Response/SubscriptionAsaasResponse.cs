using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Response
{
    public record SubscriptionAsaasResponse(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("dateCreated")] string DateCreated,
        [property: JsonPropertyName("customer")] string Customer,
        [property: JsonPropertyName("paymentLink")] string PaymentLink,
        [property: JsonPropertyName("billingType")] string BillingType,
        [property: JsonPropertyName("cycle")] string Cycle,
        [property: JsonPropertyName("value")] decimal Value,
        [property: JsonPropertyName("nextDueDate")] string NextDueDate,
        [property: JsonPropertyName("status")] string Status);
}

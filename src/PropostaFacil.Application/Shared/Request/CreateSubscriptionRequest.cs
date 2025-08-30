using PropostaFacil.Domain.Enums;
using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Request
{
    public record CreateSubscriptionRequest(
        [property: JsonPropertyName("customer")] string Customer,
        [property: JsonPropertyName("billingType")][property: JsonConverter(typeof(JsonStringEnumConverter))] BillingTypeEnum BillingType,
        [property: JsonPropertyName("value")] decimal Value,
        [property: JsonPropertyName("nextDueDate")] DateTime NextDueDate,
        [property: JsonPropertyName("cycle")] string Cycle,
        [property: JsonPropertyName("description")] string? Description
        );
}

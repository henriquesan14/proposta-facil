using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Request;

public record PaymentAsaasRequest(
    [property: JsonPropertyName("customer")] string Customer,
    [property: JsonPropertyName("value")] decimal Value,
    [property: JsonPropertyName("billingType")] string BillingType,
    [property: JsonPropertyName("dueDate")] string DueDate,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("externalReference")] string ExternalReference
    );

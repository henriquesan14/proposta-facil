using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Response
{
    public record GenerateChargeResponse(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("status")] string Status,
        [property: JsonPropertyName("invoiceUrl")] string InvoiceUrl,
        [property: JsonPropertyName("value")] decimal Value,
        [property: JsonPropertyName("dueDate")] string DueDate,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("customer")] string CustomerId,
        [property: JsonPropertyName("billingType")] string BillingType);
}

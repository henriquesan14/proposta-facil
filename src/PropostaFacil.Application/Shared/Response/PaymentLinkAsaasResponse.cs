using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Response
{
    public record PaymentAsaasResponse(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("invoiceUrl")] string InvoiceUrl);
}

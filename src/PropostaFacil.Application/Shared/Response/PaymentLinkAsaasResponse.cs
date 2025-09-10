using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Response
{
    public record PaymentLinkAsaasResponse(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("url")] string Url);
}

using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Response
{
    public record CustomerResponse(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("cpfCnpj")] string CpfCnpj,
        [property: JsonPropertyName("name")] string Name);
}

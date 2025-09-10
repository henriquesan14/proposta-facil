using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Response
{
    public record AsaasResponse<T>(
        [property: JsonPropertyName("object")] string Object,
        [property: JsonPropertyName("hasMore")] bool HasMore,
        [property: JsonPropertyName("totalCount")] int TotalCount,
        [property: JsonPropertyName("limit")] int Limit,
        [property: JsonPropertyName("offset")] int Offset,
        [property: JsonPropertyName("data")] List<T> Data);
}

using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Request;

public record CustomerAsaasRequest(
    [property: JsonPropertyName("cpfCnpj")] string CpfCnpj,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("mobilePhone")] string MobilePhone,
    [property: JsonPropertyName("notificationDisabled")] bool NotificationDisabled);

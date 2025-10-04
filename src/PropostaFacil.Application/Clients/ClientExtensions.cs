using PropostaFacil.Domain.Clients;

namespace PropostaFacil.Application.Clients;

public static class ClientExtensions
{
    public static ClientResponse ToDto(this Client client)
    {
        return new ClientResponse(
            client.Id.Value,
            client.Name,
            client.TenantId.Value,
            client.Document.Number,
            client.Contact.Email,
            client.Contact.PhoneNumber,
            client.Address.Street,
            client.Address.Number,
            client.Address.Complement,
            client.Address.District,
            client.Address.City,
            client.Address.State,
            client.Address.ZipCode
        );
    }

    public static List<ClientResponse> ToDto(this IEnumerable<Client> clients)
    {
        return clients
            .Select(ToDto)
            .ToList();
    }
}

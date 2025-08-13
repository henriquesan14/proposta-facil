using PropostaFacil.Domain.Entities;

namespace PropostaFacil.Application.Tenants
{
    public static class TenantExtensions
    {
        public static TenantResponse ToDto(this Tenant tenant)
        {
            return new TenantResponse(
                tenant.Id.Value,
                tenant.Name,
                tenant.Domain,
                tenant.Document.Number,
                tenant.Contact.Email,
                tenant.Contact.PhoneNumber,
                tenant.Address.Street,
                tenant.Address.Number,
                tenant.Address.Complement,
                tenant.Address.District,
                tenant.Address.City,
                tenant.Address.State,
                tenant.Address.ZipCode
            );
        }

        public static List<TenantResponse> ToDto(this IEnumerable<Tenant> tenants)
        {
            return tenants
                .Select(ToDto)
                .ToList();
        }
    }
}

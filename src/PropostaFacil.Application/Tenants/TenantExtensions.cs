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
                tenant.Cnpj,
                tenant.Domain
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

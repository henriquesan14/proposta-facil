using PropostaFacil.Domain.Entities;

namespace PropostaFacil.Application.Companies
{
    public static class TenantExtensions
    {
        public static TenantResponse ToDto(this Tenant company)
        {
            return new TenantResponse(
                company.Id.Value,
                company.Name,
                company.Cnpj,
                company.Domain
            );
        }

        public static List<TenantResponse> ToDto(this IEnumerable<Tenant> companies)
        {
            return companies
                .Select(ToDto)
                .ToList();
        }
    }
}

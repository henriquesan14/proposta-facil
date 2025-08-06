using PropostaFacil.Domain.Entities;

namespace PropostaFacil.Application.Companies
{
    public static class CompanyExtensions
    {
        public static CompanyResponse ToDto(this Company company)
        {
            return new CompanyResponse(
                company.Id,
                company.Name
            );
        }

        public static List<CompanyResponse> ToDto(this List<Company> companies)
        {
            return companies
                .Select(ToDto)
                .ToList();
        }
    }
}

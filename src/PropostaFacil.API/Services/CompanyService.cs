using Common.ResultPattern;
using PropostaFacil.Application.Companies;
using PropostaFacil.Domain.Entities;

namespace PropostaFacil.API.Services
{
    public class CompanyService : ICompanyService
    {
        public async Task<ResultT<IEnumerable<CompanyResponse>>> GetAsync(CancellationToken ct)
        {
            List<Company> companies = [
            new Company("Company1"),
            new Company("Company2")
        ];

            // To mock the computation delay
            await Task.Delay(1000, ct);

            return companies.ToDto();
        }

        public async Task<ResultT<CompanyResponse>> GetByIdAsync(Guid id, CancellationToken ct)
        {
            Company? configuration = null;

            // To mock the 50% success response, and 50% failure response
            if (new Random().Next(2) == 0)
            {
                configuration = new Company("Company1");
            }

            // To mock the computation delay
            await Task.Delay(1000, ct);

            if (configuration is null)
            {
                return CompanyErrors.NotFound(id.ToString());
            }
            return configuration.ToDto();
        }

        public async Task<ResultT<CompanyResponse>> AddAsync(CreateCompanyRequest request, CancellationToken ct)
        {
            var configurationExist = await IsConfigurationExistsAsync(request, ct);

            if (configurationExist)
            {
                return CompanyErrors.Conflict(request.Name.ToString());
            }

            var company = new Company(request.Name);

            var resultOfCreateConfiguration = await SaveChangesAsync(company, ct);

            if (!resultOfCreateConfiguration)
            {
                return CompanyErrors.CreateFailure;
            }

            return company.ToDto();
        }

        private static async Task<bool> IsConfigurationExistsAsync(CreateCompanyRequest request,CancellationToken ct)
        {
            // To mock the computation delay
            await Task.Delay(1000, ct);

            return request.Name == "Company1";
        }

        private static async Task<bool> SaveChangesAsync(Company company,CancellationToken ct)
        {
            // To mock the computation delay
            await Task.Delay(1000, ct);

            return company.Name == "Company3";
        }
    }

}

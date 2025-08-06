using Common.ResultPattern;
using PropostaFacil.Application.Companies;

namespace PropostaFacil.API.Services
{
    public interface ICompanyService
    {
        Task<ResultT<IEnumerable<CompanyResponse>>> GetAsync(CancellationToken ct);
        Task<ResultT<CompanyResponse>> GetByIdAsync(Guid id, CancellationToken ct);
        Task<ResultT<CompanyResponse>> AddAsync(CreateCompanyRequest request, CancellationToken ct);
    }
}

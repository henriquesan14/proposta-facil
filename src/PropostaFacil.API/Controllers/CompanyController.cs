using Microsoft.AspNetCore.Mvc;
using PropostaFacil.API.Services;
using Common.ResultPattern;
using PropostaFacil.Application.Companies;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController(ICompanyService companyService) : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await companyService.GetAsync(ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await companyService.GetByIdAsync(id, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyRequest request, CancellationToken ct)
        {
            var result = await companyService.AddAsync(request, ct);

            return result.Match(
                onSuccess: () => CreatedAtAction(
                    actionName: nameof(GetById),
                    routeValues: new { id = result.Value.Id },
                    value: result.Value
                ),
                onFailure: Problem
            );
        }
    }
}

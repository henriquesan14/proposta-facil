using Common.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Companies.Commands.CreateTenant;
using PropostaFacil.Application.Companies.Queries.GetTenants;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
    public class TenantController(IMediator mediator) : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var query = new GetTenantsQuery();
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        //[HttpGet("{id:guid}")]
        //public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        //{
        //    var result = await companyService.GetByIdAsync(id, ct);

        //    return result.Match(
        //        onSuccess: Ok,
        //        onFailure: Problem
        //    );
        //}

        [HttpPost]
        public async Task<IActionResult> Create(CreateTenantCommand command, CancellationToken ct)
        {
            var result = await mediator.Send(command, ct);

            return result.Match(
                onSuccess: () => Ok(
                    value: result.Value
                ),
                onFailure: Problem
            );
        }
    }
}

using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;
using PropostaFacil.Application.Tenants.Commands.DeleteTenant;
using PropostaFacil.Application.Tenants.Commands.UpdateTenant;
using PropostaFacil.Application.Tenants.Queries.GetTenantById;
using PropostaFacil.Application.Tenants.Queries.GetTenants;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "AdminSystem")]
    public class TenantController(IMediator mediator) : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetTenantsQuery query, CancellationToken ct)
        {
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var query = new GetTenantByIdGuery(id);
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTenantCommand command, IValidator<CreateTenantCommand> validator, CancellationToken ct)
        {
            var badRequest = ValidateOrBadRequest(command, validator);
            if (badRequest != null) return badRequest;

            var result = await mediator.Send(command, ct);

            return result.Match(
                onSuccess: () => CreatedAtAction(
                    actionName: nameof(GetById),
                    routeValues: new { id = result.Value.Id },
                    value: result.Value
                ),
                onFailure: Problem
            );
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTenantCommand command, IValidator<UpdateTenantCommand> validator, CancellationToken ct)
        {
            var badRequest = ValidateOrBadRequest(command, validator);
            if (badRequest != null) return badRequest;

            var result = await mediator.Send(command, ct);

            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var command = new DeleteTenantCommand(id);

            var result = await mediator.Send(command, ct);

            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }
    }
}

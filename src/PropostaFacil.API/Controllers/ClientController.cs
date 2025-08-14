using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Clients.Commands.CreateClient;
using PropostaFacil.Application.Clients.Queries.GetClientsByTenant;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
    public class ClientController(IMediator mediator) : BaseController
    {
        [HttpGet("{tenantId}")]
        public async Task<IActionResult> Get(Guid tenantId, CancellationToken ct)
        {
            var query = new GetClientsByTenantQuery(tenantId);
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientCommand command, IValidator<CreateTenantCommand> validator, CancellationToken ct)
        {
            //var badRequest = ValidateOrBadRequest(command, validator);
            //if (badRequest != null) return badRequest;

            var result = await mediator.Send(command, ct);

            return result.Match(
                onSuccess: () => Ok(),
                onFailure: Problem
            );
        }


    }
}

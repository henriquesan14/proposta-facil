using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Clients.Commands.CreateClient;
using PropostaFacil.Application.Clients.Commands.DeleteClient;
using PropostaFacil.Application.Clients.Queries.GetClientById;
using PropostaFacil.Application.Clients.Queries.GetClients;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;
using PropostaFacil.Application.Users.Queries.GetUserById;

namespace PropostaFacil.API.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ClientController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetClientsQuery query, CancellationToken ct)
    {
        var result = await mediator.Send(query, ct);

        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var query = new GetClientByIdQuery(id);
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
            onSuccess: () => CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { id = result.Value.Id },
                value: result.Value
            ),
            onFailure: Problem
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var command = new DeleteClientCommand(id);
        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: NoContent,
            onFailure: Problem
        );
    }
}

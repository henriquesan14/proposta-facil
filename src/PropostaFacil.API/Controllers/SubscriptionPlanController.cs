using Common.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.SubscriptionPlans.Commands.CreateSubscriptionPlan;
using PropostaFacil.Application.SubscriptionPlans.Commands.DeleteSubscriptionPlan;
using PropostaFacil.Application.SubscriptionPlans.Queries.GetSubscriptionPlanById;
using PropostaFacil.Application.SubscriptionPlans.Queries.GetSubscriptionPlans;

namespace PropostaFacil.API.Controllers;

[Route("api/[controller]")]
public class SubscriptionPlanController(IMediator mediator) : BaseController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] GetSubscriptionPlansQuery query, CancellationToken ct)
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
        var query = new GetSubscriptionPlanByIdQuery(id);
        var result = await mediator.Send(query, ct);

        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }

    [HttpPost]
    [Authorize(Roles = "AdminSystem")]
    public async Task<IActionResult> Create(CreateSubscriptionPlanCommand command, CancellationToken ct)
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
    [Authorize(Roles = "AdminSystem")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        //var badRequest = ValidateOrBadRequest(command, validator);
        //if (badRequest != null) return badRequest;
        var command = new DeleteSubscriptionPlanCommand(id);
        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: () => NoContent(),
            onFailure: Problem
        );
    }
}

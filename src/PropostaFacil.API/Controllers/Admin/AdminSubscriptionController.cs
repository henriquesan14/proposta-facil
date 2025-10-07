using Common.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Payments.Queries.GetPaymentsBySubscription;
using PropostaFacil.Application.Subscriptions.Commands.CreateSubscription;
using PropostaFacil.Application.Subscriptions.Commands.DeleteSubscription;
using PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionById;
using PropostaFacil.Application.Subscriptions.Queries.GetSubscriptions;
using PropostaFacil.Application.Tenants.Queries.GetTenantById;

namespace PropostaFacil.API.Controllers.Admin;

[Route("api/admin/subscriptions")]
[Authorize(Roles = "AdminSystem")]
public class AdminSubscriptionController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetSubscriptionsQuery query, CancellationToken ct)
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
        var query = new GetSubscriptionByIdQuery(id);
        var result = await mediator.Send(query, ct);

        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSubscriptionCommand command, CancellationToken ct)
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

    [HttpGet("{id}/payments")]
    public async Task<IActionResult> GetPayments(Guid id, CancellationToken ct)
    {
        var query = new GetPaymentsBySubscriptionQuery(id);
        var result = await mediator.Send(query, ct);

        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var command = new DeleteSubscriptionCommand(id);

        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: () => NoContent(),
            onFailure: Problem
        );
    }
}

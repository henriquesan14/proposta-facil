using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Subscriptions.Commands.CreateSubscription;
using PropostaFacil.Application.Subscriptions.Queries.GetSubscriptions;
using Common.ResultPattern;

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

    [HttpPost]
    public async Task<IActionResult> Create(CreateSubscriptionCommand command, CancellationToken ct)
    {
        //var badRequest = ValidateOrBadRequest(command, validator);
        //if (badRequest != null) return badRequest;

        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: () => Ok(result),
            onFailure: Problem
        );
    }
}

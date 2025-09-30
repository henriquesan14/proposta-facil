using Common.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionAccount;

namespace PropostaFacil.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "AdminTenant")]
public class AccountController(IMediator mediator) : BaseController
{
    [HttpGet("subscription")]
    [ProducesResponseType(typeof(SubscriptionAccountResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubscriptionAccount([FromQuery] GetSubscriptionAccountQuery query, CancellationToken ct)
    {
        var result = await mediator.Send(query, ct);

        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }
}

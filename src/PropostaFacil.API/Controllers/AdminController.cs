using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Tenants.Commands.ImpersonateTenant;
using Common.ResultPattern;
using PropostaFacil.Application.Tenants.Commands.StopImpersonateTenant;

namespace PropostaFacil.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "AdminSystem")]
public class AdminController(IMediator mediator) : BaseController
{
    [HttpPost("impersonate/{tenantId:guid}")]
    public async Task<IActionResult> ImpersonateTenant(Guid tenantId, CancellationToken ct)
    {
        var command = new ImpersonateTenantCommand(tenantId);
        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }

    [HttpPost("stop-impersonate")]
    public async Task<IActionResult> StopImpersonateTenant(CancellationToken ct)
    {
        var command = new StopImpersonateTenantCommand();
        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }
}

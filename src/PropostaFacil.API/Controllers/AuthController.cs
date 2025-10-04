using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Auth.Commands.GenerateAccessToken;
using Common.ResultPattern;
using PropostaFacil.Application.Auth.Commands.RenewRefreshToken;
using PropostaFacil.Application.Auth.Commands.RevokeRefreshToken;
using PropostaFacil.Application.Auth.Commands.ActivateAccount;
using PropostaFacil.Application.Auth.Commands.ForgotPassword;
using PropostaFacil.Application.Auth.Commands.ResetPassword;

namespace PropostaFacil.API.Controllers;

[Route("api/[controller]")]
public class AuthController(IMediator mediator) : BaseController
{

    [HttpPost("login")]
    public async Task<IActionResult> Login(GenerateAccessTokenCommand command, CancellationToken ct)
    {
        //var badRequest = ValidateOrBadRequest(command, validator);
        //if (badRequest != null) return badRequest;

        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: () => Ok(result.Value),
            onFailure: Problem
        );
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand command, CancellationToken ct)
    {
        //var badRequest = ValidateOrBadRequest(command, validator);
        //if (badRequest != null) return badRequest;

        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: () => Ok(result.Value),
            onFailure: Problem
        );
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(RevokeRefreshTokenCommand command, CancellationToken ct)
    {
        //var badRequest = ValidateOrBadRequest(command, validator);
        //if (badRequest != null) return badRequest;

        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: () => Ok(result),
            onFailure: Problem
        );
    }

    [HttpPost("activate-account")]
    public async Task<IActionResult> Activate(ActivateAccountCommand command, CancellationToken ct)
    {
        //var badRequest = ValidateOrBadRequest(command, validator);
        //if (badRequest != null) return badRequest;

        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: () => NoContent(),
            onFailure: Problem
        );
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command, CancellationToken ct)
    {
        //var badRequest = ValidateOrBadRequest(command, validator);
        //if (badRequest != null) return badRequest;

        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: () => NoContent(),
            onFailure: Problem
        );
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command, CancellationToken ct)
    {
        //var badRequest = ValidateOrBadRequest(command, validator);
        //if (badRequest != null) return badRequest;

        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: () => NoContent(),
            onFailure: Problem
        );
    }
}

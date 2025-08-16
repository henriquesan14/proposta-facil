using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Auth.Commands.GenerateAccessToken;
using Common.ResultPattern;

namespace PropostaFacil.API.Controllers
{
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
                onSuccess: () => Ok(result),
                onFailure: Problem
            );
        }
    }
}

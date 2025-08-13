using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Clients.Commands.CreateClient;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;
using Common.ResultPattern;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
    public class ClientController(IMediator mediator) : BaseController
    {

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

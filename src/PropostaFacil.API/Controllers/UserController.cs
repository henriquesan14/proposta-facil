using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;
using PropostaFacil.Application.Users.Commands.CreateUser;
using PropostaFacil.Application.Users.Queries.GetUsersByTenant;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController(IMediator mediator) : BaseController
    {
        [HttpGet("{tenantId}")]
        public async Task<IActionResult> Get(Guid tenantId, CancellationToken ct)
        {
            var query = new GetUsersByTenantQuery(tenantId);
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command, IValidator<CreateTenantCommand> validator, CancellationToken ct)
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

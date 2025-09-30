using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Users.Commands.CreateUser;
using PropostaFacil.Application.Users.Queries.GetUserById;
using PropostaFacil.Application.Users.Queries.GetUsers;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(IMediator mediator) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUsersQuery query,CancellationToken ct)
        {
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command, IValidator<CreateUserCommand> validator, CancellationToken ct)
        {
            var badRequest = ValidateOrBadRequest(command, validator);
            if (badRequest != null) return badRequest;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var query = new GetUserByIdQuery(id);
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }
    }
}

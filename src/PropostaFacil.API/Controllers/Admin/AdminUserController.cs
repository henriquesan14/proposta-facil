using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Users.Commands.AdminCreateUser;
using PropostaFacil.Application.Users.Queries.AdminGetUserById;
using PropostaFacil.Application.Users.Queries.AdminGetUsers;

namespace PropostaFacil.API.Controllers.Admin;

[Route("api/admin/users")]
[Authorize(Roles = "AdminSystem")]
public class AdminUserController(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(AdminCreateUserCommand command, IValidator<AdminCreateUserCommand> validator, CancellationToken ct)
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

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] AdminGetUsersQuery query, CancellationToken ct)
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
        var query = new AdminGetUserByIdQuery(id);
        var result = await mediator.Send(query, ct);

        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }
}

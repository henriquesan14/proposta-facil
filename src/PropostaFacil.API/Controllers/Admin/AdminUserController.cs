using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Users.Commands.AdminCreateUser;

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
            onSuccess: () => Ok(
                result.Value
            ),
            onFailure: Problem
        );
    }
}

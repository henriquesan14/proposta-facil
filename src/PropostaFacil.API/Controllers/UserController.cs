using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;
using PropostaFacil.Application.Users.Commands.CreateUser;
using PropostaFacil.Application.Users.Queries.GetUsers;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(IMediator mediator) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationRequest paginationRequest,CancellationToken ct)
        {
            var query = new GetUsersQuery(paginationRequest);
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

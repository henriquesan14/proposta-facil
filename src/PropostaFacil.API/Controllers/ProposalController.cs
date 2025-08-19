using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Proposals.Commands.CreateProposal;
using PropostaFacil.Application.Proposals.Queries.GetProposals;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProposalController(IMediator mediator) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateProposalCommand command, IValidator<CreateTenantCommand> validator, CancellationToken ct)
        {
            //var badRequest = ValidateOrBadRequest(command, validator);
            //if (badRequest != null) return badRequest;

            var result = await mediator.Send(command, ct);

            return result.Match(
                onSuccess: () => Ok(result),
                onFailure: Problem
            );
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProposalsQuery query, CancellationToken ct)
        {
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }
    }
}

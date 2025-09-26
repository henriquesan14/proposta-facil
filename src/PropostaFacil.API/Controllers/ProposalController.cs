using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Proposals;
using PropostaFacil.Application.Proposals.Commands.CreateProposal;
using PropostaFacil.Application.Proposals.Commands.SendProposal;
using PropostaFacil.Application.Proposals.Queries.GetProposalById;
using PropostaFacil.Application.Proposals.Queries.GetProposals;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProposalController(IMediator mediator) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateProposalCommand command, IValidator<CreateProposalCommand> validator, CancellationToken ct)
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
        [ProducesResponseType(typeof(IEnumerable<ProposalResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetProposalsQuery query, CancellationToken ct)
        {
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost("{id}/send")]
        public async Task<IActionResult> Send(Guid id, CancellationToken ct)
        {
            var command = new SendProposalCommand(id);
            var result = await mediator.Send(command, ct);

            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProposalResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var query = new GetProposalByIdQuery(id);
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }
    }
}

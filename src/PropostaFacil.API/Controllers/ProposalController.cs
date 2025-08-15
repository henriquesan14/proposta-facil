using Common.ResultPattern;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Proposals.Commands.CreateProposal;
using PropostaFacil.Application.Proposals.Queries.GetProposalsByTenant;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;
using PropostaFacil.Application.Tenants.Queries.GetTenants;

namespace PropostaFacil.API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{tenantId}")]
        public async Task<IActionResult> Get(Guid tenantId, CancellationToken ct)
        {
            var query = new GetProposalsByTenantQuery(tenantId);
            var result = await mediator.Send(query, ct);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }
    }
}

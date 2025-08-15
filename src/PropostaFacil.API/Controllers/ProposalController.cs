using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Proposals.Commands.CreateProposal;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;
using Common.ResultPattern;

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
                onSuccess: () => Ok(),
                onFailure: Problem
            );
        }
    }
}

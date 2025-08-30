using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Subscriptions.Commands.ActivateSubscription;

namespace PropostaFacil.API.Controllers
{
    [Route("api/webhook/asaas")]
    public class AsaasWebhookController(IMediator mediator) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post(ActivateSubscriptionCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}

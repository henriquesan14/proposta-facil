using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Payments.Commands.ConfirmPaymentSubscription;

namespace PropostaFacil.API.Controllers
{
    [Route("api/webhook/asaas")]
    public class AsaasWebhookController(IMediator mediator) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post(ConfirmPaymentSubscriptionCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }


        [HttpPost("payment-created")]
        public async Task<IActionResult> PaymentCreated(ConfirmPaymentSubscriptionCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}

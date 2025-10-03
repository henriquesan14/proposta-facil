using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaFacil.Application.Payments.Commands.PaymentCreated;
using PropostaFacil.Application.Payments.Commands.PaymentReceived;
using Common.ResultPattern;

namespace PropostaFacil.API.Controllers
{
    [Route("api/webhook/asaas")]
    public class AsaasWebhookController(IMediator mediator) : BaseController
    {
        [HttpPost("payment-received")]
        public async Task<IActionResult> Post(PaymentReceivedCommand command)
        {
            var result = await mediator.Send(command);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost("payment-created")]
        public async Task<IActionResult> PaymentCreated(PaymentCreatedCommand command)
        {
            var result = await mediator.Send(command);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }
    }
}

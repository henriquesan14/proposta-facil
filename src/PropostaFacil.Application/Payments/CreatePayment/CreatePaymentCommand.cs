using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Payments.CreatePayment
{
    public record CreatePaymentCommand : ICommand<Result>;
}

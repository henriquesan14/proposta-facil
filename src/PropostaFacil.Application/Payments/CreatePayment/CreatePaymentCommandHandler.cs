using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Payments.CreatePayment
{
    internal class CreatePaymentCommandHandler(IUnitOfWork unitOfWork, IAsaasService paymentService) : ICommandHandler<CreatePaymentCommand, Result>
    {
        public async Task<Result> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

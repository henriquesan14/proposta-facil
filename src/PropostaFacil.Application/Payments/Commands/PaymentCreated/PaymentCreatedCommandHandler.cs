using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Payments.Specifications;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Domain.Tenants.Specifications;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Payments.Commands.PaymentCreated;

public class PaymentCreatedCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService) : ICommandHandler<PaymentCreatedCommand, Result>
{
    public async Task<Result> Handle(PaymentCreatedCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payment;

        var subscription = await unitOfWork.Subscriptions
            .SingleOrDefaultAsync(new GetSubscriptionByAsaasIdSpecification(payload.Subscription));
        if (subscription is null) return PaymentErrors.NotFound(payload.Customer);

        // evitar duplicação
        var exists = await unitOfWork.Payments
            .AnyAsync(new GetPaymentByAsaasIdSpecification(payload.Id));
        if (exists) return Result.Success();

        // criar pagamento pendente no sistema
        subscription.AddPayment(
            payload.Value,
            payload.DueDate,
            payload.BillingType,
            payload.Id,
            payload.InvoiceUrl
        );

        // opcional: enviar email com link para pagamento
        var tenant = await unitOfWork.Tenants
            .SingleOrDefaultAsync(new GetTenantByIdGlobalSpecification(subscription.TenantId));

        if (tenant != null)
        {
            await emailService.SendPaymentLink(
                tenant.Contact.Email,
                tenant.Name,
                payload.InvoiceUrl,
                payload.Value,
                payload.DueDate
            );
        }

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}

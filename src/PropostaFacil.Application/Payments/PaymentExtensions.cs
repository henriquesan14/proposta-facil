using PropostaFacil.Domain.Payments;

namespace PropostaFacil.Application.Payments;

public static class PaymentExtensions
{
    public static PaymentResponse ToDto(this Payment payment)
    {
        return new PaymentResponse(
            payment.Id.Value,
            payment.SubscriptionId?.Value,
            payment.ProposalId?.Value,
            payment.Status,
            payment.Amount,
            payment.Currency,
            payment.BillingType,
            payment.PaymentDate,
            payment.DueDate,
            payment.PaymentAsaasId,
            payment.PaymentLink
        );
    }

    public static List<PaymentResponse> ToDto(this IEnumerable<Payment> payments)
    {
        return payments
            .Select(ToDto)
            .ToList();
    }
}

using Ardalis.Specification;

namespace PropostaFacil.Domain.Payments.Specifications;

public class GetPaymentByAsaasIdSpecification : SingleResultSpecification<Payment>
{
    public GetPaymentByAsaasIdSpecification(string paymentId)
    {
        Query.Where(p => p.PaymentAsaasId == paymentId);
    }
}

using Ardalis.Specification;

namespace PropostaFacil.Domain.Payments.Specifications;

public class GetPaymentByAsaasIdSpecification : Specification<Payment>
{
    public GetPaymentByAsaasIdSpecification(string paymentId)
    {
        Query.Where(p => p.PaymentAsaasId == paymentId);
    }
}

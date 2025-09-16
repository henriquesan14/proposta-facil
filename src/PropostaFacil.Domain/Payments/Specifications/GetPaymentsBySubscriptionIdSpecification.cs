using Ardalis.Specification;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Payments.Specifications;

public class GetPaymentsBySubscriptionIdSpecification : Specification<Payment>
{
    public GetPaymentsBySubscriptionIdSpecification(SubscriptionId subscriptionId)
    {
        Query.Where(p => p.SubscriptionId == subscriptionId);
    }
}

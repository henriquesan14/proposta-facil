using Ardalis.Specification;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Payments.Specifications;

public class ListPaymentsBySubscriptionIdSpecification : Specification<Payment>
{
    public ListPaymentsBySubscriptionIdSpecification(SubscriptionId subscriptionId)
    {
        Query
            .Where(p => p.SubscriptionId == subscriptionId && p.IsActive)
            .OrderByDescending(p => p.DueDate);
    }
}

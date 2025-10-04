using Ardalis.Specification;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Payments.Specifications;

public class GetLastPaymentBySubscriptionIdSpecification : SingleResultSpecification<Payment>
{
    public GetLastPaymentBySubscriptionIdSpecification(SubscriptionId subscriptionId)
    {
        Query.Where(p => p.SubscriptionId == subscriptionId)
             .OrderByDescending(p => p.DueDate)
             .Take(1);
    }
}

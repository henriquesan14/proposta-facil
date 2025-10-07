using Ardalis.Specification;
using PropostaFacil.Domain.Payments;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Payments;

public interface IPaymentRepository : IReadRepositoryBase<Payment>
{
    Task SoftDeleteBySubscriptionId(SubscriptionId subscriptionId);
}

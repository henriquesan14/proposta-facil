using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Subscriptions
{
    public interface ISubscriptionPlanRepository : IAsyncRepository<SubscriptionPlan, SubscriptionPlanId>;
}

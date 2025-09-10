using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Subscriptions
{
    public interface ISubscriptionPlanRepository
    {
        Task<SubscriptionPlan> GetByIdAsync(SubscriptionPlanId id);
        Task<IReadOnlyList<SubscriptionPlan>> GetAllByNameAsync(string name, int? pageNumber = null, int? pageSize = null);
        Task<SubscriptionPlan> GetByNameAsync(string name);
        Task<SubscriptionPlan> AddAsync(SubscriptionPlan subscriptionPlan);
        Task<int> GetCountByNameAsync(string name);
    }
}

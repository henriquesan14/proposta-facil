using Ardalis.Specification;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Subscriptions;

public interface ISubscriptionPlanRepository : IReadRepositoryBase<SubscriptionPlan>, INoSaveEfRepository<SubscriptionPlan, SubscriptionPlanId>
{
    Task<SubscriptionPlan> GetByIdAsync(SubscriptionPlanId id);
    Task<IReadOnlyList<SubscriptionPlan>> GetAllByNameAsync(string name, int? pageIndex = null, int? pageSize = null);
    Task<SubscriptionPlan> GetByNameAsync(string name);
    Task<int> GetCountByNameAsync(string name);

}

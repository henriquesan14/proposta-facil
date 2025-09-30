using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories;

public class SubscriptionPlanRepository : NoSaveEfRepository<SubscriptionPlan, SubscriptionPlanId>, ISubscriptionPlanRepository
{
    public SubscriptionPlanRepository(PropostaFacilDbContext dbContext) : base(dbContext)
    {
    }
}

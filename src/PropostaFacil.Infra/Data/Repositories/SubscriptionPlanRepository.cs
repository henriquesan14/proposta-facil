using PropostaFacil.Application.SubscriptionPlans;
using PropostaFacil.Domain.SubscriptionPlans;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories;

public class SubscriptionPlanRepository : NoSaveSoftDeleteEfRepository<SubscriptionPlan, SubscriptionPlanId>, ISubscriptionPlanRepository
{
    public SubscriptionPlanRepository(PropostaFacilDbContext dbContext) : base(dbContext)
    {
    }
}

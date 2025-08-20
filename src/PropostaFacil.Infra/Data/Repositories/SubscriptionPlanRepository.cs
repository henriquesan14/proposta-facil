using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class SubscriptionPlanRepository : RepositoryBase<SubscriptionPlan, SubscriptionPlanId>, ISubscriptionPlanRepository
    {
        public SubscriptionPlanRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}

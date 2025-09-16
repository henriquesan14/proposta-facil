using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class SubscriptionRepository : NoSaveEfRepository<Subscription, SubscriptionId>, ISubscriptionRepository
    {
        public SubscriptionRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}

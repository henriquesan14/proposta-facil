using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class SubscriptionRepository : RepositoryBase<Subscription, SubscriptionId>, ISubscriptionRepository
    {
        public SubscriptionRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}

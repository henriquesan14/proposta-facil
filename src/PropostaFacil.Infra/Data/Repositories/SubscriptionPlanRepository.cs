using Microsoft.EntityFrameworkCore;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class SubscriptionPlanRepository(PropostaFacilDbContext dbContext) : ISubscriptionPlanRepository
    {
        public async Task<SubscriptionPlan> AddAsync(SubscriptionPlan subscriptionPlan)
        {
            await dbContext.Set<SubscriptionPlan>().AddAsync(subscriptionPlan);
            return subscriptionPlan;
        }

        public async Task<SubscriptionPlan> GetByIdAsync(SubscriptionPlanId id)
        {
            return await dbContext.Set<SubscriptionPlan>().SingleAsync(e => e.Id.Equals(id));
        }

        public async Task<IReadOnlyList<SubscriptionPlan>> GetAllByNameAsync(string name, int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<SubscriptionPlan> query = dbContext.Set<SubscriptionPlan>().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(s => EF.Functions.ILike(s.Name, $"%{name}%"));
            }

            query = query.OrderBy(s => s.Name);

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<int> GetCountByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await dbContext.Set<SubscriptionPlan>().CountAsync();

            return await dbContext.Set<SubscriptionPlan>()
                .CountAsync(s => EF.Functions.ILike(s.Name, $"%{name}%"));
        }

        public async Task<SubscriptionPlan> GetByNameAsync(string name)
        {
            return await dbContext.Set<SubscriptionPlan>().FirstOrDefaultAsync(e => e.Name.Equals(name));
        }
    }
}

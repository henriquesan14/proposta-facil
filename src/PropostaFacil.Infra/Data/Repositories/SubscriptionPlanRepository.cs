using Microsoft.EntityFrameworkCore;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class SubscriptionPlanRepository : NoSaveEfRepository<SubscriptionPlan, SubscriptionPlanId>, ISubscriptionPlanRepository
    {
        public SubscriptionPlanRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<SubscriptionPlan> GetByIdAsync(SubscriptionPlanId id)
        {
            return await DbContext.Set<SubscriptionPlan>().SingleAsync(e => e.Id.Equals(id));
        }

        public async Task<IReadOnlyList<SubscriptionPlan>> GetAllByNameAsync(string name, int? pageIndex = null, int? pageSize = null)
        {
            IQueryable<SubscriptionPlan> query = DbContext.Set<SubscriptionPlan>().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(s => EF.Functions.ILike(s.Name, $"%{name}%"));
            }

            query = query.OrderBy(s => s.Name);

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                query = query
                    .Skip((pageIndex.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<int> GetCountByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await DbContext.Set<SubscriptionPlan>().CountAsync();

            return await DbContext.Set<SubscriptionPlan>()
                .CountAsync(s => EF.Functions.ILike(s.Name, $"%{name}%"));
        }

        public async Task<SubscriptionPlan> GetByNameAsync(string name)
        {
            return await DbContext.Set<SubscriptionPlan>().FirstOrDefaultAsync(e => e.Name.Equals(name));
        }
    }
}

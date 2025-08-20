using Microsoft.EntityFrameworkCore;
using PropostaFacil.Domain.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace PropostaFacil.Infra.Data
{
    public class PropostaFacilDbContext : DbContext
    {
        public PropostaFacilDbContext(DbContextOptions<PropostaFacilDbContext> options)
        : base(options) { }

        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Proposal> Proposals => Set<Proposal>();
        public DbSet<ProposalItem> ProposalItems => Set<ProposalItem>();
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("timestamp");
                    }
                }
            }

            foreach (var ownedType in builder.Model.GetEntityTypes().Where(t => t.IsOwned()))
            {
                foreach (var property in ownedType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("timestamp");
                    }
                }
            }

            ApplyGlobalIsActiveFilter(builder);

            base.OnModelCreating(builder);
        }

        private void ApplyGlobalIsActiveFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                var isActiveProp = clrType.GetProperty("IsActive");

                if (isActiveProp != null && isActiveProp.PropertyType == typeof(bool))
                {
                    var parameter = Expression.Parameter(clrType, "e");
                    var property = Expression.Property(parameter, isActiveProp);
                    var filter = Expression.Lambda(
                        Expression.Equal(property, Expression.Constant(true)),
                        parameter
                    );

                    modelBuilder.Entity(clrType).HasQueryFilter(filter);
                }
            }
        }

    }
}

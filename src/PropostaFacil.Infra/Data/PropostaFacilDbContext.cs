using Microsoft.EntityFrameworkCore;
using PropostaFacil.Domain.Clients;
using PropostaFacil.Domain.Payments;
using PropostaFacil.Domain.Proposals;
using PropostaFacil.Domain.RefreshTokens;
using PropostaFacil.Domain.SubscriptionPlans;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.ValueObjects.Ids;
using System.Reflection;

namespace PropostaFacil.Infra.Data;

public class PropostaFacilDbContext : DbContext
{
    private TenantId? _tenantId;
    public PropostaFacilDbContext(DbContextOptions<PropostaFacilDbContext> options, IUserContext currentUserService)
    : base(options) 
    {
        _tenantId = currentUserService.TenantId.HasValue ? TenantId.Of(currentUserService.TenantId!.Value) : null;
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Proposal> Proposals => Set<Proposal>();
    public DbSet<ProposalItem> ProposalItems => Set<ProposalItem>();
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<Payment> Payments => Set<Payment>();

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
        modelBuilder.Entity<Tenant>().HasQueryFilter(x => x.Id == _tenantId);
        modelBuilder.Entity<Client>().HasQueryFilter(x => x.TenantId == _tenantId);
        modelBuilder.Entity<User>().HasQueryFilter(x => x.TenantId == _tenantId);
        modelBuilder.Entity<Proposal>().HasQueryFilter(x => x.TenantId == _tenantId);
        modelBuilder.Entity<Subscription>().HasQueryFilter(x => x.TenantId == _tenantId);
    }
}

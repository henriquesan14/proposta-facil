using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => SubscriptionId.Of(value));

            builder.Property(s => s.TenantId)
                .HasConversion(
                    id => id.Value,
                    value => TenantId.Of(value))
                .IsRequired();

            builder.Property(s => s.SubscriptionPlanId)
                .HasConversion(
                    id => id.Value,
                    value => SubscriptionPlanId.Of(value))
                .IsRequired();

            builder.Property(s => s.StartDate)
                .IsRequired();

            builder.Property(s => s.EndDate);

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(s => s.ProposalsUsed)
                .IsRequired();

            builder.HasOne(s => s.Tenant)
                .WithMany(t => t.Subscriptions)
                .HasForeignKey(s => s.TenantId);

            builder.HasOne(s => s.SubscriptionPlan)
                .WithMany(sp => sp.Subscriptions)
                .HasForeignKey(s => s.SubscriptionPlanId);

            builder.HasMany(s => s.Payments)
                .WithOne(p => p.Subscription)
                .HasForeignKey(p => p.SubscriptionId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaFacil.Domain.SubscriptionPlans;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Configurations
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.ToTable("SubscriptionPlans");

            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Id)
                .HasConversion(
                    id => id.Value,
                    value => SubscriptionPlanId.Of(value));

            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(sp => sp.Name)
                .IsUnique();

            builder.Property(sp => sp.MaxProposalsPerMonth)
                .IsRequired();

            builder.Property(sp => sp.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(sp => sp.Description)
                .HasMaxLength(1000);

            builder.HasMany(sp => sp.Subscriptions)
                .WithOne(s => s.SubscriptionPlan)
                .HasForeignKey(s => s.SubscriptionPlanId);
        }
    }
}

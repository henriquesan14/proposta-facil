using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, value => PaymentId.Of(value));

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Currency)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(p => p.BillingType)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.DueDate)
                .IsRequired();

            builder.Property(p => p.ExternalReference)
                .HasMaxLength(200);

            builder.HasOne(p => p.Subscription)
                   .WithMany(s => s.Payments)
                   .HasForeignKey(p => p.SubscriptionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Proposal)
                   .WithOne(p => p.Payment)
                   .HasForeignKey<Payment>(p => p.ProposalId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

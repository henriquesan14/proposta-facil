using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaFacil.Domain.Payments;
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

            builder.Property(p => p.PaymentDate)
                .HasColumnType("date")
                .IsRequired(false);

            builder.Property(p => p.DueDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.PaymentAsaasId)
                .HasMaxLength(200);

            builder.Property(p => p.PaymentLink)
                .HasMaxLength(300);

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

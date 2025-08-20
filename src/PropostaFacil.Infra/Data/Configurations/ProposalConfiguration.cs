using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Configurations
{
    public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.ToTable("Proposals");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, value => ProposalId.Of(value))
                .ValueGeneratedNever();

            builder.Property(p => p.TenantId)
                .HasConversion(id => id.Value, value => TenantId.Of(value))
                .IsRequired();

            builder.Property(p => p.ClientId)
                .HasConversion(id => id.Value, value => ClientId.Of(value))
                .IsRequired();

            builder.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(50);

            builder
            .HasIndex(p => p.Number)
            .IsUnique();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.ProposalStatus)
                .IsRequired();

            builder.OwnsOne(p => p.TotalAmount, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasMaxLength(3)
                    .IsRequired();
            });

            builder.Property(p => p.ValidUntil)
                .IsRequired();

            builder.HasOne(p => p.Tenant)
                .WithMany()
                .HasForeignKey(p => p.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Client)
                .WithMany(c => c.Proposals)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.HasMany(p => p.Items)
                .WithOne(i => i.Proposal)
                .HasForeignKey(i => i.ProposalId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

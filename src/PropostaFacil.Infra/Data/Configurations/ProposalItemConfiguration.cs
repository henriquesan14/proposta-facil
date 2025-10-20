using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaFacil.Domain.Proposals;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Configurations;

public class ProposalItemConfiguration : IEntityTypeConfiguration<ProposalItem>
{
    public void Configure(EntityTypeBuilder<ProposalItem> builder)
    {
        builder.ToTable("ProposalItems");

        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.Id)
            .HasConversion(id => id.Value, value => ProposalItemId.Of(value))
            .ValueGeneratedNever();

        builder.Property(pi => pi.ProposalId)
            .HasConversion(id => id.Value, value => ProposalId.Of(value))
            .IsRequired();

        builder.Property(pi => pi.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(pi => pi.Description)
            .HasMaxLength(1000);

        builder.Property(pi => pi.Quantity)
            .IsRequired();

        builder.Property(pi => pi.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Ignore(pi => pi.TotalPrice);

        builder.HasOne(pi => pi.Proposal)
            .WithMany(p => p.Items)
            .HasForeignKey(pi => pi.ProposalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

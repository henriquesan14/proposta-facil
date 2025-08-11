using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects;

namespace PropostaFacil.Infra.Data.Configurations
{
    public class EnqueteConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasConversion(
                    id => id.Value,
                    value => TenantId.Of(value)
                );

            builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(d => d.Cnpj)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(d => d.Domain)
                .IsRequired()
                .HasMaxLength(300);

        }
    }
}

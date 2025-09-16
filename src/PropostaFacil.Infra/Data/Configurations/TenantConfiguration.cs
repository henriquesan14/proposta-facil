using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Configurations
{
    public class EnqueteConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasConversion(
                    id => id.Value,
                    value => TenantId.Of(value)
                );

            builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

            builder.OwnsOne(c => c.Document, doc =>
            {
                doc.Property(d => d.Number)
                   .IsRequired()
                   .HasMaxLength(20);

                doc.HasIndex(d => d.Number).IsUnique();
            });

            builder.OwnsOne(c => c.Contact, contact =>
            {
                contact.Property(cn => cn.Email)
                    .HasMaxLength(255);

                contact.Property(cn => cn.PhoneNumber)
                    .HasMaxLength(20);
            });

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Street).HasMaxLength(200);
                address.Property(a => a.Number).HasMaxLength(20);
                address.Property(a => a.Complement).HasMaxLength(100);
                address.Property(a => a.District).HasMaxLength(100);
                address.Property(a => a.City).HasMaxLength(100);
                address.Property(a => a.State).HasMaxLength(2);
                address.Property(a => a.ZipCode).HasMaxLength(10);
            });

            builder.Property(d => d.Domain)
                .IsRequired()
                .HasMaxLength(300);

            builder.HasMany(t => t.Clients)
                .WithOne(c => c.Tenant)
                .HasForeignKey(c => c.TenantId);

            builder.HasMany(t => t.Users)
                .WithOne(c => c.Tenant)
                .HasForeignKey(c => c.TenantId);

            builder.HasMany(t => t.Subscriptions)
                .WithOne(c => c.Tenant)
                .HasForeignKey(c => c.TenantId);

        }
    }
}

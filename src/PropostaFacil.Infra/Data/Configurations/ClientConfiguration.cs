using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaFacil.Domain.Clients;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasConversion(
                    id => id.Value,
                    value => ClientId.Of(value));

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.OwnsOne(c => c.Document, doc =>
            {
                doc.Property(d => d.Number)
                   .IsRequired()
                   .HasMaxLength(20);

            });

            builder.OwnsOne(c => c.Contact, contact =>
            {
                contact.Property(cn => cn.Email)
                    .HasMaxLength(255);

                contact.HasIndex(c => c.Email)
                    .IsUnique();

                contact.Property(cn => cn.PhoneNumber)
                    .HasMaxLength(20);
            });

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Street).HasMaxLength(200);
                address.Property(a => a.Number).HasMaxLength(20);
                address.Property(a => a.Complement).HasMaxLength(100).IsRequired(false);
                address.Property(a => a.District).HasMaxLength(100);
                address.Property(a => a.City).HasMaxLength(100);
                address.Property(a => a.State).HasMaxLength(2);
                address.Property(a => a.ZipCode).HasMaxLength(10);
            });

            builder.Property(c => c.TenantId)
                .HasConversion(
                    id => id.Value,
                    value => TenantId.Of(value))
                .IsRequired();

            builder.HasMany(c => c.Proposals)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

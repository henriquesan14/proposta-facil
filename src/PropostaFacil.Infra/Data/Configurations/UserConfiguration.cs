using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Of(value));

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.OwnsOne(c => c.Contact, contact =>
        {
            contact.Property(cn => cn.Email)
                .HasMaxLength(255);

            contact.HasIndex(c => c.Email)
                .IsUnique();

            contact.Property(cn => cn.PhoneNumber)
                .HasMaxLength(20);
        });

        builder.Property(u => u.PasswordHash)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(u => u.TenantId)
            .HasConversion(
                id => id.Value,
                value => TenantId.Of(value));

        builder.HasOne(u => u.Tenant)
            .WithMany(t => t.Users)
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

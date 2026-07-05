using Chamtsere.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chamtsere.Infrastructure.Data.Configurations.UserConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.UserName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(b => b.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(b => b.Phone)
            .HasMaxLength(20);
        builder.Property(b => b.FirstName)
            .HasMaxLength(50);
        builder.Property(b => b.LastName)
            .HasMaxLength(50);
        builder.Property(b => b.ExternalAuthId)
            .HasMaxLength(100);

        builder.HasIndex(b => b.ExternalAuthId)
            .IsUnique(false);
    }
}

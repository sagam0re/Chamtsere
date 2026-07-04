using Chamtsere.Domain.Entities.User.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chamtsere.Infrastructure.Data.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<CommonUser>
{
    public void Configure(EntityTypeBuilder<CommonUser> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.UserName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(b => b.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(b => b.Phone)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(b => b.FirstName)
            .HasMaxLength(50);
        builder.Property(b => b.LastName)
            .HasMaxLength(50);
        builder.Property(b => b.ExternalAuthId)
            .HasMaxLength(100);
    }
}

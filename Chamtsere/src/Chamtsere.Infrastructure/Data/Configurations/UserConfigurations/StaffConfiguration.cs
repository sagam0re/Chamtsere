using Chamtsere.Domain.Entities.User.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chamtsere.Infrastructure.Data.Configurations.User;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.TenantId)
            .IsRequired();

        builder.HasIndex(b => b.TenantId)
            .IsUnique(false);
    }
}

using Chamtsere.Domain.Entities.StaffProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chamtsere.Infrastructure.Data.Configurations.StaffProfileConfigurations;

public class StaffProfileConfiguration : IEntityTypeConfiguration<StaffProfile>
{
    public void Configure(EntityTypeBuilder<StaffProfile> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.TenantId).IsRequired();
        builder.Property(b => b.JobTitle).HasMaxLength(100);

        builder.HasIndex(b => b.UserId);
        builder.HasIndex(b => b.TenantId);
    }
}

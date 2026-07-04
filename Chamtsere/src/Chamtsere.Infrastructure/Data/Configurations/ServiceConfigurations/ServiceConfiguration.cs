using Chamtsere.Domain.Entities.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chamtsere.Infrastructure.Data.Configurations.ServiceConfigurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).HasMaxLength(256);
        builder.Property(s => s.Duration).HasDefaultValue(0);
        builder.Property(s => s.Price).HasMaxLength(256);
    }
}

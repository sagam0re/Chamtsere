using Chamtsere.Domain.Entities.SalonService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chamtsere.Infrastructure.Data.Configurations;

public class SalonServiceConfiguration : IEntityTypeConfiguration<SalonService>
{
    public void Configure(EntityTypeBuilder<SalonService> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Description).HasMaxLength(256);
        builder.Property(s => s.Categories).IsRequired();
    }
}

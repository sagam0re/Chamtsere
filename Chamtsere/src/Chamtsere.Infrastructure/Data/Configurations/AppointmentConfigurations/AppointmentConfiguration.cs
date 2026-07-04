using Chamtsere.Domain.Entities.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chamtsere.Infrastructure.Data.Configurations.AppointmentConfigurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.TenantId).IsRequired();
        builder.Property(b => b.CustomerId).IsRequired();
        builder.Property(b => b.StaffId).IsRequired();
        builder.Property(b => b.StartTime).IsRequired();
        builder.Property(b => b.EndTime).IsRequired();
        builder.Property(b => b.TransactionId).IsRequired();
        builder.Property(b => b.Status).IsRequired();
        builder.Property(b => b.TotalPrice).IsRequired();

        builder.Property(b => b.SalonServiceIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
            )
            .IsRequired();

        builder.HasIndex(b => b.TenantId);
    }
}

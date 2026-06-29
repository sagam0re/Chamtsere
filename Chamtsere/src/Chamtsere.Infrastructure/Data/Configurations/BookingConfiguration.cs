using Chamtsere.Domain.Entities.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chamtsere.Infrastructure.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.CustomerId).IsRequired();
        builder.Property(b => b.EmployeeId).IsRequired();
        builder.Property(b => b.Date).IsRequired();
        builder.Property(b => b.TransactionId).IsRequired();
        builder.Property(b => b.IsDeleted).HasDefaultValue(false);
        builder.Property(b => b.DeletedOnUtc);
        builder.Property(b => b.DeletedBy);
    }
}

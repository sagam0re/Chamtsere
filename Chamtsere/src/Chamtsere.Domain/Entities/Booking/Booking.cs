using Chamtsere.Domain.Common;

namespace Chamtsere.Domain.Entities.Booking;

public class Booking : BaseAuditableEntity
{
    public Guid CustomerId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public required List<Guid> SalonServiceIds { get; set; }
    public Guid TransactionId { get; set; }
}

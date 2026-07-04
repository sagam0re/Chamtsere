using Chamtsere.Domain.Common;

namespace Chamtsere.Domain.Entities.Appointment;

public class Appointment : BaseAuditableEntity
{
    public Guid TenantId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid StaffId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public required List<Guid> SalonServiceIds { get; set; }
    public Guid TransactionId { get; set; }
    public Status Status { get; set; }
    public required string TotalPrice { get; set; }
}

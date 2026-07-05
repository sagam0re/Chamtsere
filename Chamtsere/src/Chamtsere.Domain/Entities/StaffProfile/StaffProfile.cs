using Chamtsere.Domain.Common;

namespace Chamtsere.Domain.Entities.StaffProfile;

public class StaffProfile : BaseAuditableEntity
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string? JobTitle { get; set; }
}

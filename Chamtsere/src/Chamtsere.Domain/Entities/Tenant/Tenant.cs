using Chamtsere.Domain.Common;

namespace Chamtsere.Domain.Entities.Tenant;

public class Tenant : BaseAuditableEntity
{
    public required string Name { get; set; }
}

using Chamtsere.Domain.Entities.User.Common;

namespace Chamtsere.Domain.Entities.User.Staff;

public class Staff : CommonUser
{
    public Guid TenantId { get; set; }
}

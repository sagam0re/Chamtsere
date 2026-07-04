using Chamtsere.Domain.Common;

namespace Chamtsere.Domain.Entities.User.Common;

public abstract class CommonUser : BaseAuditableEntity
{
    public required string UserName { get; set; } = null!;
    public required string Email { get; set; } = null!;
    public required string Phone { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ExternalAuthId { get; private set; }
}

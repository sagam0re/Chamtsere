using Chamtsere.Domain.Common;

namespace Chamtsere.Domain.Entities.User;

public class User : BaseAuditableEntity
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ExternalAuthId { get; private set; }

    public void SetExternalAuthId(string externalAuthId)
    {
        ExternalAuthId = externalAuthId;
    }
}

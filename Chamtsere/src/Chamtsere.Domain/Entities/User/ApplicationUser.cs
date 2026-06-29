using Microsoft.AspNetCore.Identity;

namespace Chamtsere.Domain.Entities.User;

public class ApplicationUser : IdentityUser
{
    public required string FirstName { get; set; } = null!;
    public required string LastName { get; set; } = null!;
}


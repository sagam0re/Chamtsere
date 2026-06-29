using Chamtsere.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace Chamtsere.Domain.Entities.Token;

public class Token
{
    [Key]
    public Guid Id { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public required string UserId { get; set; }

    // Navigation property to the associated user
    public virtual ApplicationUser? User { get; set; }
}

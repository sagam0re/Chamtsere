namespace Chamtsere.Domain.Common;

public abstract class BaseEntity : ISoftDeletable
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public string? DeletedBy { get; set; }
}
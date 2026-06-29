namespace Chamtsere.Domain.Common;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    DateTime? DeletedOnUtc { get; set; }
    string? DeletedBy { get; set; }
}

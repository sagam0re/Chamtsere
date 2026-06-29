using Chamtsere.Domain.Common;

namespace Chamtsere.Domain.Entities.Transaction;

public class Transaction : BaseAuditableEntity
{
    public TransactionType TransactionType { get; set; }
    public required string Amount { get; set; }
    public bool IsPaid { get; set; }
}

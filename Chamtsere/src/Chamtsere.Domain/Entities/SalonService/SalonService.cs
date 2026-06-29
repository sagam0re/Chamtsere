using Chamtsere.Domain.Common;

namespace Chamtsere.Domain.Entities.SalonService;

public class SalonService : BaseAuditableEntity
{
    public string Description { get; set; } = null!;
    public List<SalonServiceType> Categories { get; set; } = [];
}

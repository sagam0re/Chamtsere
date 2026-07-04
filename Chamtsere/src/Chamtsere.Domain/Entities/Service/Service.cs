using Chamtsere.Domain.Common;

namespace Chamtsere.Domain.Entities.Service;

public class Service : BaseAuditableEntity
{
    public required string Name { get; set; }
    public int Duration { get; set; }
    public required string Price { get; set; }
    /*public List<ServiceType> Categories { get; set; } = [];*/
}

namespace Chamtsere.Domain.Entities.SalonService;

public record ServiceCategory
{
    public SalonServiceType Description { get; set; }
    public required string Time { get; set; }
}

using Chamtsere.Domain.Entities.SalonService;

namespace Chamtsere.Application.Features.SalonServiceFeature.Commands;

public record SalonServiceCommand : IRequest<Guid>
{
    public required string Description { get; set; }
    public List<SalonServiceType>? Categories { get; set; }
}

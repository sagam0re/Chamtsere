using Chamtsere.Domain.Entities.Service;

namespace Chamtsere.Application.Features.SalonServiceFeature.Commands;

public record SalonServiceCommand : IRequest<Guid>
{
    public required string Description { get; set; }
    public List<ServiceType>? Categories { get; set; }
}

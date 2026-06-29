using Chamtsere.Application.Common.Interfaces;
using Chamtsere.Domain.Entities.SalonService;

namespace Chamtsere.Application.Features.SalonServiceFeature.Commands;

public class SalonServiceCommandHandler : IRequestHandler<SalonServiceCommand, Guid>
{
    private readonly IChamtsereDbContext _dbContext;
    private readonly IIdentityService _identityService;

    public SalonServiceCommandHandler(
        IChamtsereDbContext dbContext,
        IIdentityService identityService
        )
    {
        _dbContext = dbContext;
        _identityService = identityService;
    }

    public async Task<Guid> Handle(SalonServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = new SalonService
        {
            Description = request.Description,
            Categories = request.Categories ?? []
        };

        _dbContext.SalonServices.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

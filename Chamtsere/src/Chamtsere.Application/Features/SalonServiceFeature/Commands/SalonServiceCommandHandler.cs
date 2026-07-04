using Chamtsere.Application.Common.Interfaces;

namespace Chamtsere.Application.Features.SalonServiceFeature.Commands;

public class SalonServiceCommandHandler : IRequestHandler<SalonServiceCommand, Guid>
{
    private readonly IChamtsereDbContext _dbContext;

    public SalonServiceCommandHandler(
        IChamtsereDbContext dbContext
        )
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(SalonServiceCommand request, CancellationToken cancellationToken)
    {
        return await _dbContext.Services
            .Where(x => x.Name == request.Description)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}

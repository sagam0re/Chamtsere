using Chamtsere.Application.Common.Interfaces;

namespace Chamtsere.Application.Features.UserFeature.Queries.UserList;

public class UserListQueryHandler : IRequestHandler<UserListQuery, List<UserListQueryResult>>
{
    private readonly IIdentityService _identityService;

    public UserListQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task<List<UserListQueryResult>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        var users = _identityService.GetAllAsync();

        if (users == null)
        {
            return new List<UserListQueryResult>();
        }

        return await users.ToListAsync(cancellationToken);
    }
}

using Chamtsere.Application.Common.Interfaces;

namespace Chamtsere.Application.Features.UserFeature.Queries.UserList;

public class UserListQueryHandler : IRequestHandler<UserListQuery, List<UserListQueryResult>>
{
    private readonly ICurrentUser _user;

    public UserListQueryHandler(ICurrentUser user)
    {
        _user = user;
    }
    public async Task<List<UserListQueryResult>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        var user = _user;
        return new List<UserListQueryResult>
        {
            new UserListQueryResult
            {
                Id = "1",
                UserName = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                Email = ""}
        };
    }
}


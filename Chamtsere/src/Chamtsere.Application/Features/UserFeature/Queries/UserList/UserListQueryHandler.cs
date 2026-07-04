namespace Chamtsere.Application.Features.UserFeature.Queries.UserList;

public class UserListQueryHandler : IRequestHandler<UserListQuery, List<UserListQueryResult>>
{

    public UserListQueryHandler()
    {
    }
    public async Task<List<UserListQueryResult>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
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

namespace Chamtsere.Application.Features.UserFeature.Queries.UserList;

public record UserListQueryResult
{
    public string Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public IList<string> Roles { get; set; } = new List<string>();
}

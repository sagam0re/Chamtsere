namespace Chamtsere.Application.Features.UserFeature.Commands.Create;

public record CreateUserCommand : IRequest<string>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}

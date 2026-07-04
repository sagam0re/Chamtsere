namespace Chamtsere.Application.Features.UserFeature.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{

    public CreateUserCommandHandler()
    {
    }
    public async Task<string> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        return await Task.FromResult("User created successfully");
    }
}

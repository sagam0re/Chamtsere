using Chamtsere.Application.Common.Interfaces;

namespace Chamtsere.Application.Features.UserFeature.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task<string> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var (Result, UserId) = await _identityService.CreateUserAsync(command);
        if (Result.Errors.Length > 0)
            throw new InvalidOperationException("Failed to create user.");

        return UserId;
    }
}

using Chamtsere.Application.Common.Models;

namespace Chamtsere.Application.Features.UserFeature.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result>
{

    public LoginCommandHandler()
    {
    }
    public async Task<Result> Handle(LoginCommand command, CancellationToken cancellationToken)
    {

        return Result.Success("Logged In Successfully");

    }
}

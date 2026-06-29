using Chamtsere.Application.Common.Models;

namespace Chamtsere.Application.Features.UserFeature.Commands.Login;

public class LoginCommand : IRequest<Result>
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}

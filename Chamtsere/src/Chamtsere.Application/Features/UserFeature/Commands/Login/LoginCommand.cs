using Chamtsere.Application.Common.Models;

namespace Chamtsere.Application.Features.UserFeature.Commands.Login;

public class LoginCommand : IRequest<Result>
{
    public string Email { get; set; }
    public string UserName { get; set; }
}

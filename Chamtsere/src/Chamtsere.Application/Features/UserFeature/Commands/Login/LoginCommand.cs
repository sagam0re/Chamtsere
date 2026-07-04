using Chamtsere.Application.Common.Models;

namespace Chamtsere.Application.Features.UserFeature.Commands.Login;

public class LoginCommand : IRequest<Result>
{
    /* public string UserName { get; set; }
     public string Password { get; set; }*/
    public required string ExternalAuthId { get; set; }
}

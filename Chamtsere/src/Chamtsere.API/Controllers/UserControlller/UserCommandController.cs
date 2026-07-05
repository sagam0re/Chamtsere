using Chamtsere.Application.Features.UserFeature.Commands.Login;
using Microsoft.AspNetCore.Mvc;

namespace Chamtsere.API.Controllers.UserControlller;

public partial class UserController
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)

    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}

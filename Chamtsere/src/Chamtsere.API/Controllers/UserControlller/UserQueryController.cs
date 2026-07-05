using Chamtsere.Application.Features.UserFeature.Queries.UserList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chamtsere.API.Controllers.UserControlller;

public partial class UserController
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> List([FromQuery] UserListQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}

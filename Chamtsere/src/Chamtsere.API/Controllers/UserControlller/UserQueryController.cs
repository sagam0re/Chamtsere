using Chamtsere.Application.Features.UserFeature.Queries.UserList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chamtsere.API.Controllers.UserControlller;

public partial class UserController
{
    [HttpGet("get-all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers([FromQuery] UserListQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}

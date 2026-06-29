using Chamtsere.Application.Features.SalonServiceFeature.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chamtsere.API.Controllers.SalonServiceController;

public partial class SalonServiceController
{
    [HttpPost("create")]
    //[ProducesResponseType(typeof(SalonServiceResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateSalonService([FromBody] SalonServiceCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(id);
    }
}

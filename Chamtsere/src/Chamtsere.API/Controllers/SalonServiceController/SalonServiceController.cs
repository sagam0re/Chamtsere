using Microsoft.AspNetCore.Mvc;

namespace Chamtsere.API.Controllers.SalonServiceController;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public partial class SalonServiceController(IMediator mediator) : ControllerBase;

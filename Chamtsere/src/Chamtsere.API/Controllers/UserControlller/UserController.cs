using Microsoft.AspNetCore.Mvc;

namespace Chamtsere.API.Controllers.UserControlller;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public partial class UserController(IMediator mediator) : ControllerBase;

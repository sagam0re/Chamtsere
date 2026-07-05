using Microsoft.AspNetCore.Mvc;

namespace Chamtsere.API.Controllers.UserControlller;

[ApiController]
[Route("API/[controller]/[action]")]
[Produces("application/json")]
public partial class UserController(IMediator mediator) : ControllerBase;

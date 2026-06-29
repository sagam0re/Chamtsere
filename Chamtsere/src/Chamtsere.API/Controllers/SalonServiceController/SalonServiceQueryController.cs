using Microsoft.AspNetCore.Mvc;

namespace Chamtsere.API.Controllers.SalonServiceController;

public partial class SalonServiceController
{
    [HttpGet("list")]
    public async Task<IActionResult> GetAllSalonServices()
    {
        //var salonServices = await _salonServiceService.GetAllSalonServicesAsync();
        return Ok("Greate");
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Chamtsere.API.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

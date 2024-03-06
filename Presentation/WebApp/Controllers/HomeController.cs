using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    [HttpGet("/")]
    [HttpGet("about")]
    public IActionResult About()
    {
        return View();
    }

    public IActionResult RouteNotFounded()
    {
        return View();
    }
}
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.Home.Controllers;
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Home()
    {
        return View();
    }
}

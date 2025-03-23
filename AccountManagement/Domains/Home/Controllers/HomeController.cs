using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.Home.Controllers;

public class HomeController : Controller
{
    public IActionResult Home()
    {
        return View();
    }
}

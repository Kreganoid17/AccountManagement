using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.About.Controllers;

public class AboutController : Controller
{
    public IActionResult About()
    {
        return View();
    }
}

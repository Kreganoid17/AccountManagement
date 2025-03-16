using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.Contact.Controllers;

public class ContactController : Controller
{
    public IActionResult Contact()
    {
        return View();
    }
}

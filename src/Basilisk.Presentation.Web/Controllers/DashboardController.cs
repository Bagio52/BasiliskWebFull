using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers;

public class DashboardController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Dashboard/Index.cshtml");
    }
}

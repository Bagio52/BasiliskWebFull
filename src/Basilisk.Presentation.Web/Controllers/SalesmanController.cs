using System;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers;
[Route("[controller]/[action]")]
public class SalesmanController : Controller
{
    private readonly SalesmanService _service;

    public SalesmanController(SalesmanService service) {
        _service = service;
    }

    [HttpGet]
    public IActionResult Insert() {
        return View(new SalesmanInsertViewModel {
            SalesmanDropdown = _service.GetSalesmanDropDown()
        });
    }

    [HttpPost]
    public IActionResult Insert(SalesmanViewModel salesman)
    {
        if (!ModelState.IsValid)
        {
            return View(new SalesmanInsertViewModel
            {
                Salesman = salesman,
                SalesmanDropdown = _service.GetSalesmanDropDown()
            });
        }
        _service.InsertSalesman(salesman);
        return RedirectToPage("");
    }
}

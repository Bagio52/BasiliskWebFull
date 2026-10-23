using System;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers;

[Microsoft.AspNetCore.Mvc.Route("[controller]/[action]")]
public class CustomerController : Controller
{
    private readonly ILogger<CustomerController> _logger;
    private readonly CustomerService _service;

    public CustomerController(ILogger<CustomerController> logger, CustomerService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult Index(string? companyName, string? contactName)
    {
        var model = string.IsNullOrWhiteSpace(companyName)
                 && string.IsNullOrWhiteSpace(contactName)
            ? _service.GetCustomers()
            : _service.SearchCustomer(companyName, contactName);

        ViewBag.SearchName = companyName;
        ViewBag.SearchContact = contactName;

        return View(model);
    }

    [HttpGet]
    public IActionResult Insert()
    {
        return View(new CustomerViewModel());
    }
}   

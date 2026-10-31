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
    public IActionResult Index(string? companyName, string? contactPerson, int page = 1)
    {
        var model = string.IsNullOrWhiteSpace(companyName)
                 && string.IsNullOrWhiteSpace(contactPerson)
            ? _service.GetCustomers()
            : _service.SearchCustomer(companyName, contactPerson);

        // Mengatur jumlah data tampil
        // View jumlah tampilan data
        const int pageSize = 10;
        var totalCount = model.Customers.Count;
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)pageSize));
        page = Math.Clamp(page, 1, totalPages);

        model.Customers = model.Customers
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        model.PageNumber = page;
        model.TotalPages = totalPages;

        ViewBag.SearchName = companyName;
        ViewBag.SearchContact = contactPerson;

        return View(model);
    }

    [HttpGet]
    public IActionResult Insert()
    {
        return View(new CustomerInsertViewModel
        {
            Customer = new CustomerViewModel()
        });
    }

    [HttpPost]
    public IActionResult Insert(CustomerInsertViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _service.InsertCustomer(model.Customer);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        var model = _service.GetCustomerById(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(CustomerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var updated = _service.UpdateCustomer(model);

        if (!updated)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(long id)
    {
        var deleted = _service.DeleteCustomer(id);

        if (!deleted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}   

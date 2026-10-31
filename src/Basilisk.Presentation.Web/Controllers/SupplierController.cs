using System;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels.Supplier;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers;

[Route("[controller]/[action]")]
public class SupplierController : Controller
{
    private readonly ILogger<SupplierController> _logger;
    private readonly SupplierService _service;

    public SupplierController(ILogger<SupplierController> logger, SupplierService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult Index(
        string? searchCompanyName,
        string? searchContactPerson,
        string? searchJobTitle,
        int page = 1)
    {
        var companyName = searchCompanyName?.Trim();
        var contactPerson = searchContactPerson?.Trim();
        var jobTitle = searchJobTitle?.Trim();

        var hasSearch = !string.IsNullOrEmpty(companyName)
            || !string.IsNullOrEmpty(contactPerson)
            || !string.IsNullOrEmpty(jobTitle);
        var model = hasSearch
            ? _service.SearchSuppliers(
                companyName ?? string.Empty,
                contactPerson ?? string.Empty,
                jobTitle ?? string.Empty)
            : _service.GetSuppliers();

        const int pageSize = 10;
        var totalCount = model.Suppliers.Count;
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)pageSize));
        page = Math.Clamp(page, 1, totalPages);
        model.Suppliers = model.Suppliers
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        model.PageNumber = page;
        model.TotalPages = totalPages;

        ViewBag.SearchCompanyName = searchCompanyName;
        ViewBag.SearchContactPerson = searchContactPerson;
        ViewBag.SearchJobTitle = searchJobTitle;

        return View(model);
    }

    [HttpGet]
    public IActionResult Insert()
    {
        return View(new SupplierInsertViewModel
        {
            Supplier = new SupplierViewModel()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Insert(SupplierInsertViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        _service.InsertSupplier(model.Supplier);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        var model = _service.GetSupplierById(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(SupplierViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var updated = _service.UpdateSupplier(model);

        if (!updated)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(long id)
    {
        var deleted = _service.DeleteSupplier(id);

        if (!deleted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}

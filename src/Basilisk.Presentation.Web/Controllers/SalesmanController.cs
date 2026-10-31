using System;
using Basilisk.DataAccess.Models.Enums;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Presentation.Web.Controllers;
[Route("[controller]/[action]")]
public class SalesmanController : Controller
{
    private readonly SalesmanService _service;

    public SalesmanController(SalesmanService service) {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index(
        string? employeeNumber,
        string? fullName,
        Level? level,
        string? superiorSearch,
        int page = 1)
    {
        employeeNumber = employeeNumber?.Trim();
        fullName = fullName?.Trim();
        superiorSearch = superiorSearch?.Trim();

        var model = _service.GetSalesmans(
            employeeNumber,
            fullName,
            level,
            superiorSearch);
        model.Levels = Enum.GetValues(typeof(Level))
            .Cast<Level>()
            .Select(salesmanLevel => new SelectListItem
            {
                Value = salesmanLevel.ToString(),
                Text = salesmanLevel.ToString().Replace('_', ' '),
                Selected = level == salesmanLevel
            })
            .ToList();

        const int pageSize = 10;
        var totalCount = model.Salesmans.Count;
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)pageSize));
        page = Math.Clamp(page, 1, totalPages);
        model.Salesmans = model.Salesmans
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        model.PageNumber = page;
        model.TotalPages = totalPages;

        return View(model);
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

    [HttpGet]
    public IActionResult Edit(string employeeNumber)
    {
        var salesman = _service.GetSalesmanById(employeeNumber);
        if (salesman == null)
        {
            return NotFound();
        }

        return View(new SalesmanInsertViewModel
        {
            Salesman = salesman,
            SalesmanDropdown = GetSuperiorOptions(employeeNumber, salesman.SuperiorEmployeeNumber)
        });
    }

    [HttpPost]
    public IActionResult Edit(string employeeNumber, SalesmanInsertViewModel model)
    {
        ModelState.Remove("Salesman.EmployeeNumber");
        model.Salesman.EmployeeNumber = employeeNumber;

        if (!ModelState.IsValid)
        {
            model.SalesmanDropdown = GetSuperiorOptions(
                employeeNumber,
                model.Salesman.SuperiorEmployeeNumber);
            return View(model);
        }

        var updated = _service.UpdateSalesman(model.Salesman);
        if (!updated)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(string employeeNumber)
    {
        var deleted = _service.DeleteSalesman(employeeNumber);
        if (!deleted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    private List<SelectListItem> GetSuperiorOptions(
        string employeeNumber,
        string? selectedSuperiorEmployeeNumber)
    {
        return _service.GetAllSalesmans()
            .Where(salesman => salesman.EmployeeNumber != employeeNumber)
            .Select(superior => new SelectListItem
            {
                Value = superior.EmployeeNumber,
                Text = $"{superior.FullName} ({superior.EmployeeNumber})",
                Selected = superior.EmployeeNumber == selectedSuperiorEmployeeNumber
            })
            .ToList();
    }
}

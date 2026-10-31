using System;
using System.Drawing;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels.Region;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers;
[Microsoft.AspNetCore.Mvc.Route("[controller]/[action]")]
public class RegionController : Controller
{
    private readonly ILogger<RegionController> _logger;
    private readonly RegionService _service;

    public RegionController(ILogger<RegionController> logger, RegionService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult Index(string? searchCity, int page = 1)
    {
        var model = string.IsNullOrWhiteSpace(searchCity)
            ? _service.GetRegion()
            : _service.SearchRegionByCity(searchCity);

        // Mengatur jumlah data tampil
        const int pageSize = 10;
            var totalCount = model.Regions.Count;
            var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)pageSize));
            page = Math.Clamp(page, 1, totalPages);

            model.Regions = model.Regions
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            model.PageNumber = page;
            model.TotalPages = totalPages;


        ViewBag.SearchCity = searchCity;

        return View(model);
    }

    [HttpGet]
    public IActionResult Insert()
    {
        return View(new RegionInsertViewModel
        {
            Regions = new RegionViewModel()
        });
    }

    [HttpPost]
    public IActionResult Insert(RegionInsertViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _service.InsertRegion(model.Regions);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        var model = _service.GetRegionById(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(RegionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var updated = _service.UpdateRegion(model);

        if (!updated)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(long id)
    {
        var deleted = _service.DeleteRegion(id);

        if (!deleted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}

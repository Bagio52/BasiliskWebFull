using System;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels.Delivery;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers;
[Route("[controller]/[action]")]
public class DeliveryController : Controller
{
    private readonly ILogger<DeliveryController> _logger;
    private readonly DeliveryService _service;

    public DeliveryController(ILogger<DeliveryController> logger, DeliveryService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult Index(string? searchCompanyName, int page = 1)
    {
        var model = string.IsNullOrWhiteSpace(searchCompanyName)
            ? _service.GetDelivieries()
            : _service.SearchDeliveries(searchCompanyName);

        const int pageSize = 10;
            var totalCount = model.Deliveries.Count;
            var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)pageSize));
            page = Math.Clamp(page, 1, totalPages);

            model.Deliveries = model.Deliveries
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            model.PageNumber = page;
            model.TotalPages = totalPages;

        ViewBag.SearchCompanyName = searchCompanyName;

        return View(model);
    }

    [HttpGet]
    public IActionResult Insert()
    {
        return View(new DeliveryInsertViewModel
        {
            Deliveries = new DeliveryViewModel()
        });
    }

    [HttpPost]
    public IActionResult Insert(DeliveryInsertViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _service.InsertDelivery(model.Deliveries);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        var model = _service.GetDeliveryById(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(DeliveryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var updated = _service.UpdateDelivery(model);

        if (!updated)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(long id)
    {
        var deleted = _service.DeleteDelivery(id);

        if (!deleted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}

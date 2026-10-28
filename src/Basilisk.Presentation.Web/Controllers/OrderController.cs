using System;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers;

[Microsoft.AspNetCore.Mvc.Route("[controller]/[action]")]
public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly OrderService _service;
    private readonly CustomerService _customerService;
    private readonly SalesmanService _salesmanService;
    private readonly DeliveryService _deliveryService;

    public OrderController(
        ILogger<OrderController> logger, 
        OrderService service,
        CustomerService customerService,
        SalesmanService salesmanService,
        DeliveryService deliveryService)
    {
        _logger = logger;
        _service = service;
        _customerService = customerService;
        _salesmanService = salesmanService;
        _deliveryService = deliveryService;
    }

    [HttpGet]
    public IActionResult Index(
        string? invoiceNumber, 
        long? customerId,
        long? salesmanId,
        long? deliveryId,
        int page = 1)
    {
        var model = string.IsNullOrWhiteSpace(invoiceNumber)
            ? _service.GetOrders()
            : _service.SearchOrders(invoiceNumber);

        const int pageSize = 10;
            var totalCount = model.Orders.Count;
            var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)pageSize));
            page = Math.Clamp(page, 1, totalPages);

            model.Orders = model.Orders
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            model.PageNumber = page;
            model.TotalPages = totalPages;

        ViewBag.InvoiceNumber = invoiceNumber;

        return View(model);
    }

    [HttpGet]
    public IActionResult Insert()
    {
        return View(new OrderInsertViewModel
        {
            Order = new OrderViewModel()
        });
    }

    [HttpPost]
    public IActionResult Insert(OrderInsertViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _service.InsertOrder(model.Order);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(string invoiceNumber)
    {
        var model = _service.GetOrderById(invoiceNumber);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(OrderViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var updated = _service.UpdateOrder(model);

        if (!updated)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(string invoiceNumber)
    {
        var deleted = _service.DeleteOrder(invoiceNumber);

        if (!deleted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}

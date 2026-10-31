using System;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        string? employeeNumber,
        long? deliveryId,
        int page = 1)
    {
        invoiceNumber = invoiceNumber?.Trim();
        employeeNumber = employeeNumber?.Trim();

        var customers = _customerService.GetAllCustomers();
        var salesmans = _salesmanService.GetAllSalesmans();
        var deliveries = _deliveryService.GetAllDeliveries();
        var model = _service.GetOrders();
        model.CustomerId = customerId;
        model.EmployeeNumber = employeeNumber;
        model.DeliveryId = deliveryId;

        model.Customers = customers.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.CompanyName,
            Selected = c.Id == customerId
        }).ToList();

        model.Salesmans = salesmans.Select(s => new SelectListItem
        {
            Value = s.EmployeeNumber,
            Text = $"{s.FirstName} {s.LastName}".Trim(),
            Selected = s.EmployeeNumber == employeeNumber
        }).ToList();

        model.Deliveries = deliveries.Select(d => new SelectListItem
        {
            Value = d.Id.ToString(),
            Text = d.CompanyName,
            Selected = d.Id == deliveryId
        }).ToList();

        model.Orders = model.Orders
            .Where(o =>
                (!customerId.HasValue || o.CustomerId == customerId.Value) &&
                (string.IsNullOrEmpty(employeeNumber) || o.SalesEmployeeNumber == employeeNumber) &&
                (!deliveryId.HasValue || o.DeliveryId == deliveryId.Value) &&
                (string.IsNullOrEmpty(invoiceNumber) ||
                    o.InvoiceNumber.Contains(invoiceNumber, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        // Mengatur jumlah data tampil
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

    private void PopulateOrderOptions(OrderInsertViewModel model)
    {
        model.Customers = _customerService.GetAllCustomers()
            .Select(customer => new SelectListItem
            {
                Value = customer.Id.ToString(),
                Text = customer.CompanyName
            })
            .ToList();

        model.Salesmans = _salesmanService.GetAllSalesmans()
            .Select(salesman => new SelectListItem
            {
                Value = salesman.EmployeeNumber,
                Text = $"{salesman.FirstName} {salesman.LastName}".Trim()
            })
            .ToList();

        model.Deliveries = _deliveryService.GetAllDeliveries()
            .Select(delivery => new SelectListItem
            {
                Value = delivery.Id.ToString(),
                Text = delivery.CompanyName
            })
            .ToList();
    }
}

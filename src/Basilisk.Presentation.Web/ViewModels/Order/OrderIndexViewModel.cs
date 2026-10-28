using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Presentation.Web.ViewModels.Order;

public class OrderIndexViewModel
{ 
    public List<OrderViewModel> Orders { get; set; }

    // Selected Item
    public long? CustomerId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public long? DeliveryId { get; set; }
    public List<SelectListItem> Customers { get; set; } = new();
    public List<SelectListItem> Salesmans { get; set; } = new();
    public List<SelectListItem> Deliveries { get; set; } = new();

    // Tampilan jumlah data pada index
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }

}

using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Presentation.Web.ViewModels.Order;

public class OrderInsertViewModel
{
    public OrderViewModel Order { get; set; } = new();
    public List<SelectListItem> Customers { get; set; } = new();
    public List<SelectListItem> Salesmans { get; set; } = new();
    public List<SelectListItem> Deliveries { get; set; } = new();
} 

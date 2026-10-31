using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Presentation.Web.ViewModels;

public class ProductIndexViewModel
{
    public List<ProductViewModel> Products { get; set; } = new();
    public string? Name { get; set; }
    public long? CategoryId { get; set; }
    public long? SupplierId { get; set; }

    // Data pilihan dropdown dari database
    public List<SelectListItem> Categories { get; set; } = new();
    public List<SelectListItem> Suppliers { get; set; } = new();
}

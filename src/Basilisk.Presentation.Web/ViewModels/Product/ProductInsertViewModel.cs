using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Presentation.Web.ViewModels;

public class ProductInsertViewModel
{
    public ProductViewModel Products { get; set; }
    public List<SelectListItem> Categories { get; set; } = new();
    public List<SelectListItem> Suppliers { get; set; } = new();

}

using System;

namespace Basilisk.Presentation.Web.ViewModels;

public class CategoryIndexViewModel
{
    public List<CategoryViewModel> Categories { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
}

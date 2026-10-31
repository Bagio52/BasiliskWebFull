using System;

namespace Basilisk.Presentation.Web.ViewModels.Supplier;

public class SupplierIndexViewModel
{
    public List<SupplierViewModel> Suppliers { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
}

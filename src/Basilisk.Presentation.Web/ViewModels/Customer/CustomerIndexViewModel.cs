using System;

namespace Basilisk.Presentation.Web.ViewModels.Customer;

public class CustomerIndexViewModel
{
    public List<CustomerViewModel> Customers { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
}

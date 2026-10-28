using System;

namespace Basilisk.Presentation.Web.ViewModels.Delivery;

public class DeliveryViewModel
{
    public long Id { get; set; }
    public string CompanyName { get; set; }
    public string? Phone { get; set; }
    public decimal Cost { get; set; }
}

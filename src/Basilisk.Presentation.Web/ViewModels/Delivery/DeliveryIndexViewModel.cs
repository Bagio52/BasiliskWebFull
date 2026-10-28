using System;

namespace Basilisk.Presentation.Web.ViewModels.Delivery;

public class DeliveryIndexViewModel
{
    public List<DeliveryViewModel> Deliveries { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
}

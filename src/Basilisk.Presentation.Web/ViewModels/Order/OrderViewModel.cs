using System;

namespace Basilisk.Presentation.Web.ViewModels.Order;
 
public class OrderViewModel
{
    public string InvoiceNumber { get; set; }
    public long CustomerId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string SalesEmployeeNumber { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DueDate { get; set; }
    public long DeliveryId { get; set; }
    public string DeliveryCompanyName { get; set; } = string.Empty;
    public decimal DeliveryCost { get; set; }
    public string DestinationAddress { get; set; }
    public string DestinationCity { get; set; }
    public string DestinationPostalCode { get; set; }
}

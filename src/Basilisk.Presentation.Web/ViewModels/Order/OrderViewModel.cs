using System;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace Basilisk.Presentation.Web.ViewModels.Order;
 
public class OrderViewModel
{
    [Required]
    public string InvoiceNumber { get; set; }

    [Range(1, long.MaxValue)]
    public long CustomerId { get; set; }

    [ValidateNever]
    public string CompanyName { get; set; } = string.Empty;
    
    [Required]
    public string SalesEmployeeNumber { get; set; }
    
    [ValidateNever]
    public string FirstName { get; set; } = string.Empty;
    
    [ValidateNever]
    public string LastName { get; set; } = string.Empty;
    
    [Range(typeof(DateTime), "1753-01-01", "9999-12-31")]
    public DateTime OrderDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DueDate { get; set; }
    
    [Range(1, long.MaxValue)]
    public long DeliveryId { get; set; }
    
    [ValidateNever]
    public string DeliveryCompanyName { get; set; } = string.Empty;
    public decimal DeliveryCost { get; set; }
    
    [Required]
    public string DestinationAddress { get; set; }
    
    [Required]
    public string DestinationCity { get; set; }
    
    [Required]
    public string DestinationPostalCode { get; set; }
}

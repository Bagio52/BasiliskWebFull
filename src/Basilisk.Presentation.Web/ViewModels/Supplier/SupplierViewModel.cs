using System;

namespace Basilisk.Presentation.Web.ViewModels.Supplier;

public class SupplierViewModel
{
    public long Id { get; set; }
    public string? CompanyName { get; set; } = null!;
    public string? ContactPerson { get; set; } = null!;
    public string? JobTitle { get; set; } = null!;

    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime? DeleteDate { get; set; }

    public virtual ICollection<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

    public override string ToString()
    {
        return $"Id: {Id}, CompanyName: {CompanyName}, ContactPerson: {ContactPerson}, JobTitle: {JobTitle}, Address: {Address}, City: {City}, Phone: {Phone}, Email: {Email}, DeleteDate: {DeleteDate}";
    }
}

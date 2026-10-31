using System;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.ViewModels.Supplier;

public class SupplierViewModel
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Company name wajib diisi.")]
    public string CompanyName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contact person wajib diisi.")]
    public string ContactPerson { get; set; } = string.Empty;

    [Required(ErrorMessage = "Job title wajib diisi.")]
    public string JobTitle { get; set; } = string.Empty;

    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }

    [EmailAddress(ErrorMessage = "Format email tidak valid.")]
    public string? Email { get; set; }
    public DateTime? DeleteDate { get; set; }

    public virtual ICollection<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

    public override string ToString()
    {
        return $"Id: {Id}, CompanyName: {CompanyName}, ContactPerson: {ContactPerson}, JobTitle: {JobTitle}, Address: {Address}, City: {City}, Phone: {Phone}, Email: {Email}, DeleteDate: {DeleteDate}";
    }
}

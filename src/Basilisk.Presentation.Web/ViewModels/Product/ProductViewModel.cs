using System;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.ViewModels;

public class ProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long? SupplierId { get; set; }
    public string? SupplierName { get; set; }

    [Range(1, long.MaxValue, ErrorMessage = "Pilih kategori produk.")]
    public long CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Range(0, int.MaxValue)]
    public int OnOrder { get; set; }
    public bool Discontinued { get; set; }
    public string? ImagePath { get; set;}

    public override string? ToString()
    {
        return  $"ID            : {Id} \n" +
                $"\tName        : {Name} \n" +
                $"\tSupplier ID : {SupplierId} \n" +
                $"\tCategory ID : {CategoryId} \n" +
                $"\tDescription : {Description} \n" +
                $"\tPrice       : {Price} \n" +
                $"\tStock       : {Stock} \n" +
                $"\tOn Order    : {OnOrder} \n" +
                $"\tDiscontinued: {Discontinued} \n" +
                $"\tImage Path  : {ImagePath}";
    }
}

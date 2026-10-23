using System;

namespace Basilisk.Presentation.Web.ViewModels;

public class ProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long? SupplierId { get; set; }
    public long CategoryId { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
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

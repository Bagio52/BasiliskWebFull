using System;

namespace Basilisk.Presentation.Web.ViewModels;

public class CategoryViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public override string? ToString()
    {
        return  $"ID            : {Id} \n" +
                $"\tName        : {Name} \n" +
                $"\tDescription : {Description}";
    }
}

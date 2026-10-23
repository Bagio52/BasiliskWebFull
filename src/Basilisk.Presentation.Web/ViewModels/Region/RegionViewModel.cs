using System;

namespace Basilisk.Presentation.Web.ViewModels.Region;

public class RegionViewModel
{
    public long Id { get; set; }
    public string City { get; set; }
    public string? Remark { get; set; }

    public override string? ToString()
    {
        return $"ID       : {Id} \n"+
               $"\tCity   : {City} \n" +
               $"\tRemark : {Remark} \n";
    }
}

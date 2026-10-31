using System;

namespace Basilisk.Presentation.Web.ViewModels.Region;

public class RegionIndexViewModel
{
    public List<RegionViewModel> Regions { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
}   

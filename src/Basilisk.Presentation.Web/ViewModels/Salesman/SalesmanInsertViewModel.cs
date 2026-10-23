using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Presentation.Web.ViewModels;

public class SalesmanInsertViewModel
{
    public SalesmanViewModel Salesman { get; set; }
    public List<SelectListItem> SalesmanDropdown { get; set; }
}

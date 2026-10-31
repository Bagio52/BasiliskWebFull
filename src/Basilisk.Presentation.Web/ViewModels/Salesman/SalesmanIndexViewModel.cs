using System;

namespace Basilisk.Presentation.Web.ViewModels.Salesman;

using Basilisk.DataAccess.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

public class SalesmanIndexViewModel
{
    public List<SalesmanViewModel> Salesmans { get; set; } = new();
    public string? EmployeeNumber { get; set; }
    public string? FullName { get; set; }
    public Level? Level { get; set; }
    public string? SuperiorSearch { get; set; }
    public List<SelectListItem> Levels { get; set; } = new();
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
}

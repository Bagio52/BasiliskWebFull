using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Presentation.Web.Services;

public class SalesmanService
{
    private readonly ISalesmanRepository _salesmanRepository;

    public SalesmanService(ISalesmanRepository salesmanRepository)
    {
        _salesmanRepository = salesmanRepository;
    }

    public List<SelectListItem> GetSalesmanDropDown()
    {
        return _salesmanRepository.GetAllSalesman()
            .Select(i => new SelectListItem
            {
                Value = i.EmployeeNumber,
                Text = i.FullName
            }
            ).ToList();
    }

    public void InsertSalesman(SalesmanViewModel vm)
    {
        _salesmanRepository.AddSalesman(new Salesman
        {
            EmployeeNumber = vm.EmployeeNumber,
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            Level = vm.Level.Value,
            BirthDate = vm.BirthDate.Value,
            HiredDate = vm.HiredDate,
            Address = vm.Address,
            City = vm.City,
            Phone = vm.Phone,
            SuperiorEmployeeNumber = vm.SuperiorEmployeeNumber
        });
    }
}

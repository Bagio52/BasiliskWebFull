using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;
using Basilisk.DataAccess.Models.Enums;
using Basilisk.Presentation.Web.ViewModels;
using Basilisk.Presentation.Web.ViewModels.Salesman;
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

    public SalesmanIndexViewModel GetSalesmans(
        string? employeeNumber = null,
        string? fullName = null,
        Level? level = null,
        string? superiorSearch = null)
    {
        var salesmen = _salesmanRepository.GetAllSalesman();
        var namesByEmployeeNumber = salesmen.ToDictionary(
            s => s.EmployeeNumber,
            s => s.FullName);

        var filteredSalesmen = salesmen
            .Where(s => string.IsNullOrWhiteSpace(employeeNumber) ||
                s.EmployeeNumber.Contains(employeeNumber, StringComparison.OrdinalIgnoreCase))
            .Where(s => string.IsNullOrWhiteSpace(fullName) ||
                s.FullName.Contains(fullName, StringComparison.OrdinalIgnoreCase))
            .Where(s => !level.HasValue || s.Level == level.Value)
            .Where(s => string.IsNullOrWhiteSpace(superiorSearch) ||
                (s.SuperiorEmployeeNumber != null &&
                    (s.SuperiorEmployeeNumber.Contains(
                         superiorSearch,
                         StringComparison.OrdinalIgnoreCase) ||
                     (namesByEmployeeNumber.TryGetValue(
                          s.SuperiorEmployeeNumber,
                          out var superiorFullName) &&
                      superiorFullName.Contains(
                          superiorSearch,
                          StringComparison.OrdinalIgnoreCase)))))
            .Select(s => MapSalesman(s, namesByEmployeeNumber))
            .ToList();

        return new SalesmanIndexViewModel
        {
            Salesmans = filteredSalesmen,
            EmployeeNumber = employeeNumber,
            FullName = fullName,
            Level = level,
            SuperiorSearch = superiorSearch
        };
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

    public SalesmanViewModel? GetSalesmanById(string employeeNumber)
    {
        var salesman = _salesmanRepository.GetSalesmanById(employeeNumber);

        if (salesman == null)
        {
            return null;
        }

        var salesmen = _salesmanRepository.GetAllSalesman();
        var namesByEmployeeNumber = salesmen.ToDictionary(
            s => s.EmployeeNumber,
            s => s.FullName);
        return MapSalesman(salesman, namesByEmployeeNumber);
    }

    public bool UpdateSalesman(SalesmanViewModel vm)
    {
        if (!vm.Level.HasValue || !vm.BirthDate.HasValue)
        {
            return false;
        }

        var salesman = _salesmanRepository.GetSalesmanById(vm.EmployeeNumber);

        if (salesman == null)
        {
            return false;
        }

        salesman.EmployeeNumber = vm.EmployeeNumber;
        salesman.FirstName = vm.FirstName;
        salesman.LastName = vm.LastName;
        salesman.Level = vm.Level.Value;
        salesman.BirthDate = vm.BirthDate.Value;
        salesman.HiredDate = vm.HiredDate;
        salesman.Address = vm.Address;
        salesman.City = vm.City;
        salesman.Phone = vm.Phone;
        salesman.SuperiorEmployeeNumber = vm.SuperiorEmployeeNumber;

        _salesmanRepository.UpdateSalesman(salesman);

        return true;
    }

    public bool DeleteSalesman(string employeeNumber)
    {
        return _salesmanRepository.DeleteSalesman(employeeNumber);
    }

    private static SalesmanViewModel MapSalesman(
        Salesman salesman,
        IReadOnlyDictionary<string, string> namesByEmployeeNumber)
    {
        return new SalesmanViewModel
        {
            EmployeeNumber = salesman.EmployeeNumber,
            FirstName = salesman.FirstName,
            LastName = salesman.LastName ?? string.Empty,
            Level = salesman.Level,
            BirthDate = salesman.BirthDate,
            HiredDate = salesman.HiredDate,
            Address = salesman.Address ?? string.Empty,
            City = salesman.City ?? string.Empty,
            Phone = salesman.Phone ?? string.Empty,
            SuperiorEmployeeNumber = salesman.SuperiorEmployeeNumber,
            SuperiorFullName = salesman.SuperiorEmployeeNumber != null &&
                namesByEmployeeNumber.TryGetValue(salesman.SuperiorEmployeeNumber, out var superiorFullName)
                    ? superiorFullName
                    : null
        };
    }

    public List<Salesman> GetAllSalesmans()
    {
        return _salesmanRepository.GetAllSalesman();
    }
}

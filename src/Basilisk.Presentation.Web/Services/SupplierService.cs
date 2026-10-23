using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels.Supplier;

namespace Basilisk.Presentation.Web.Services;

public class SupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public SupplierIndexViewModel GetSuppliers()
    {
        return new SupplierIndexViewModel()
        {
            Suppliers = _supplierRepository.GetAllSuppliers()
            .Select(s => new SupplierViewModel()
            {
                Id = s.Id,
                CompanyName = s.CompanyName,
                ContactPerson = s.ContactPerson,
                JobTitle = s.JobTitle
            }).ToList()
        };
    }

    public void InsertSupplier(SupplierViewModel vm)
    {
        _supplierRepository.AddSupplier(new Supplier
        {
            CompanyName = vm.CompanyName,
            ContactPerson = vm.ContactPerson,
            JobTitle = vm.JobTitle,
            Address = vm.Address,
            City = vm.City,
            Phone = vm.Phone,
            Email = vm.Email,
            DeleteDate = vm.DeleteDate
        });
    }

    public SupplierViewModel? GetSupplierById(long id)
    {
        var supplier = _supplierRepository.GetSupplierById(id);
        if (supplier == null)
        {
            return null;
        }

        return new SupplierViewModel
        {
            Id = supplier.Id,
            CompanyName = supplier.CompanyName,
            ContactPerson = supplier.ContactPerson,
            JobTitle = supplier.JobTitle,
            Address = supplier.Address,
            City = supplier.City,
            Phone = supplier.Phone,
            Email = supplier.Email,
            DeleteDate = supplier.DeleteDate
        };
    }

    public bool UpdateSupplier(SupplierViewModel vm)
    {
        var supplier = _supplierRepository.GetSupplierById(vm.Id);
        if (supplier == null)
        {
            return false;
        }

        supplier.CompanyName = vm.CompanyName;
        supplier.ContactPerson = vm.ContactPerson;
        supplier.JobTitle = vm.JobTitle;
        supplier.Address = vm.Address;
        supplier.City = vm.City;
        supplier.Phone = vm.Phone;
        supplier.Email = vm.Email;
        supplier.DeleteDate = vm.DeleteDate;

        _supplierRepository.UpdateSupplier(supplier);
        return true;
    }

    public bool DeleteSupplier(long id)
    {
        return _supplierRepository.DeleteSupplier(id);
    }


    public SupplierIndexViewModel SearchSuppliers(string companyName, string contactPerson, string jobTitle)
    {

        return new SupplierIndexViewModel
        {
            Suppliers = _supplierRepository
                .SearchSuppliers(companyName, contactPerson, jobTitle)
                .Select(s => new SupplierViewModel
                {
                    Id = s.Id,
                    CompanyName = s.CompanyName,
                    ContactPerson = s.ContactPerson,
                    JobTitle = s.JobTitle,
                }).ToList()
        };
    }

}

using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels.Customer;

namespace Basilisk.Presentation.Web.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public CustomerIndexViewModel GetCustomers()
    {
        return new CustomerIndexViewModel()
        {
            Customers = _customerRepository.GetAllCustomers()
            .Select(c => new CustomerViewModel()
            {
                Id = c.Id,
                CompanyName = c.CompanyName,
                ContactPerson = c.ContactPerson,
                Address = c.Address,
                City = c.City,
                Phone = c.Phone,
                Email = c.Email,
                DeleteDate = c.DeleteDate
            }).ToList()
        };
    }

    public void InsertCustomer(CustomerViewModel vm)
    {
        _customerRepository.AddCustomer(new Customer
        {
            CompanyName = vm.CompanyName,
            ContactPerson = vm.ContactPerson,
            Address = vm.Address,
            City = vm.City,
            Phone = vm.Phone,
            Email = vm.Email,
            DeleteDate = vm.DeleteDate
        });
    }

    public CustomerViewModel? GetCustomerById(long id)
    {
        var customer = _customerRepository.GetCustomerById(id);

        if (customer == null)
        {
            return null;
        }

        return new CustomerViewModel
        {
            Id = customer.Id,
            CompanyName = customer.CompanyName,
            ContactPerson = customer.ContactPerson,
            Address = customer.Address,
            City = customer.City,
            Phone = customer.Phone,
            Email = customer.Email,
            DeleteDate = customer.DeleteDate
        };
    }

    public bool UpdateCustomer(CustomerViewModel vm)
    {
        var customer = _customerRepository.GetCustomerById(vm.Id);
        if (customer == null)
        {
            return false;
        }

        customer.CompanyName = vm.CompanyName;
        customer.ContactPerson = vm.ContactPerson;
        customer.Address = vm.Address;
        customer.City = vm.City;
        customer.Phone = vm.Phone;
        customer.Email = vm.Email;
        customer.DeleteDate = vm.DeleteDate;

        _customerRepository.UpdateCustomer(customer);

        return true;
    }

    public bool DeleteCustomer(long id)
    {
        return _customerRepository.DeleteCustomer(id);
    }

    public CustomerIndexViewModel SearchCustomer(string? companyName, string? contactPerson)
    {
        return new CustomerIndexViewModel
        {
            Customers = _customerRepository
                .SearchCustomers(companyName, contactPerson)
                .Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    ContactPerson = c.ContactPerson,
                    Address = c.Address,
                    City = c.City,
                    Phone = c.Phone,
                    Email = c.Email,
                    DeleteDate = c.DeleteDate
                })
                .ToList()
        };
    }

    public List<Customer> GetAllCustomers()
    {
        return _customerRepository.GetAllCustomers();
    }
}

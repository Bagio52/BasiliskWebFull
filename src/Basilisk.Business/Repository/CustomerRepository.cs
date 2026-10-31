using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly BasiliskTFContext _context;

    public CustomerRepository(BasiliskTFContext context)
    {
        _context = context;
    }

    public List<Customer> GetAllCustomers()
    {
        return _context.Customers.ToList();
    }

    public Customer? GetCustomerById(long id)
    {
        return _context.Customers.Find(id);
    }

    public Customer AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();

        return customer;
    }

    public Customer UpdateCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
        _context.SaveChanges();

        return customer;
    }

    public bool DeleteCustomer(long id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        _context.SaveChanges();

        return true;
    }

    public IEnumerable<Customer> SearchCustomers(string? companyName, string? contactPerson)
    {
        var query = _context.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(companyName))
        {
            query = query.Where(c => c.CompanyName.Contains(companyName));
        }

        if (!string.IsNullOrWhiteSpace(contactPerson))
        {
            query = query.Where(c => c.ContactPerson.Contains(contactPerson));
        }

        return query.ToList();
    }

}

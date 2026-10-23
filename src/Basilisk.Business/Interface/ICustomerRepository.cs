using System;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Interface;

public interface ICustomerRepository
{
    List<Customer> GetAllCustomers();
    Customer? GetCustomerById(long id);
    Customer AddCustomer(Customer customer);
    Customer UpdateCustomer(Customer customer); 
    bool DeleteCustomer(long id);
    IEnumerable<Customer> SearchCustomers(string companyName, string contactName);
}

using System;
using Basilisk.DataAccess.Models;
namespace Basilisk.Business.Interface;

public interface ISupplierRepository
{
    List<Supplier> GetAllSuppliers();
    Supplier? GetSupplierById(long id);
    Supplier AddSupplier(Supplier supplier);
    Supplier UpdateSupplier(Supplier supplier);
    bool DeleteSupplier(long id);
    IEnumerable<Supplier> SearchSuppliers(string companyName, string contactPerson, string jobTitle);
}

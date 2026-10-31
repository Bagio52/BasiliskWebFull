using System;
using Basilisk.DataAccess.Models;
using Basilisk.Business.Interface;

namespace Basilisk.Business.Repository;

public class SupplierRepository : ISupplierRepository
{
    private readonly BasiliskTFContext _context;

    public SupplierRepository(BasiliskTFContext context)
    {
        _context = context;
    }

    public List<Supplier> GetAllSuppliers()
    {
        return _context.Suppliers.ToList();
    }

    public Supplier? GetSupplierById(long id)
    {
        return _context.Suppliers.Find(id);
    }

    public Supplier AddSupplier(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        _context.SaveChanges();

        return supplier;
    }

    public Supplier UpdateSupplier(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
        _context.SaveChanges();

        return supplier;
    }

    public bool DeleteSupplier(long id)
    {
        var supplier = _context.Suppliers.Find(id);

        if (supplier == null)
        {
            return false;
        }

        var relatedProducts = _context.Products
            .Where(product => product.SupplierId == id)
            .ToList();
        foreach (var product in relatedProducts)
        {
            product.SupplierId = null;
        }

        _context.Suppliers.Remove(supplier);
        _context.SaveChanges();

        return true;
    }

    public IEnumerable<Supplier> SearchSuppliers(string companyName, string contactPerson, string jobTitle)
    {
        var query = _context.Suppliers.AsQueryable();

        if (!string.IsNullOrEmpty(companyName))
        {
            query = query.Where(s => s.CompanyName.Contains(companyName));
        }

        if (!string.IsNullOrEmpty(contactPerson))
        {
            query = query.Where(s => s.ContactPerson.Contains(contactPerson));
        }

        if (!string.IsNullOrEmpty(jobTitle))
        {
            query = query.Where(s => s.JobTitle.Contains(jobTitle));
        }

        return query.ToList();
    }

}

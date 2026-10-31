using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Repository;

public class SalesmanRepository : ISalesmanRepository
{
    private readonly BasiliskTFContext _context;

    public SalesmanRepository(BasiliskTFContext context)
    {
        _context =  context;
    }

    public List<Salesman> GetAllSalesman()
    {
        return _context.Salesmen.ToList();
    }

    public Salesman? GetSalesmanById(string employeeNumber)
    {
        return _context.Salesmen
            .FirstOrDefault(s => s.EmployeeNumber == employeeNumber);
    }

    public Salesman AddSalesman(Salesman salesman)
    {
        _context.Add(salesman);
        _context.SaveChanges();
        return salesman;
    }

    public Salesman UpdateSalesman(Salesman salesman)
    {
        _context.Salesmen.Update(salesman);
        _context.SaveChanges();
        return salesman;
    }

    public bool DeleteSalesman(string employeeNumber)
    {
        var salesman = GetSalesmanById(employeeNumber);
        if (salesman == null)
        {
            return false;
        }

        _context.Salesmen.Remove(salesman);
        _context.SaveChanges();
        return true;
    }
}

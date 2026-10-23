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

    public Salesman GetSalesmanById(int id)
    {
        throw new NotImplementedException();
    }

    public Salesman AddSalesman(Salesman salesman)
    {
        _context.Add(salesman);
        _context.SaveChanges();
        return salesman;
    }

    public Salesman UpdateSalesman(Salesman salesman)
    {
        throw new NotImplementedException();
    }

    public Salesman DeleteSalesman(Salesman salesman)
    {
        throw new NotImplementedException();
    }
}

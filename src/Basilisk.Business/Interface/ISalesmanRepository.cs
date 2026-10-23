using System;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Interface;

public interface ISalesmanRepository
{
    List<Salesman> GetAllSalesman();
    Salesman GetSalesmanById(int id);
    Salesman AddSalesman(Salesman salesman);
    Salesman UpdateSalesman(Salesman salesman);
    Salesman DeleteSalesman(Salesman salesman);
}

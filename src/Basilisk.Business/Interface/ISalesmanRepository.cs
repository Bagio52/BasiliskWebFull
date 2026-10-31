using System;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Interface;

public interface ISalesmanRepository
{
    List<Salesman> GetAllSalesman();
    Salesman GetSalesmanById(string employeeNumber);
    Salesman AddSalesman(Salesman salesman);
    Salesman UpdateSalesman(Salesman salesman);
    bool DeleteSalesman(string employeeNumber);
}

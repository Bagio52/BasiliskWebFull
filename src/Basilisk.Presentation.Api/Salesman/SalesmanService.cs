using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;

namespace Basilisk.Presentation.Api.Salesman;

public class SalesmanService
{
    private readonly ISalesmanRepository _salesmanRepository;

    public SalesmanService(ISalesmanRepository salesmanRepository)
    {
        _salesmanRepository = salesmanRepository;
    }

    public SalesmanDto InsertSalesman(SalesmanDto dto)
    {
        _salesmanRepository.AddSalesman(new DataAccess.Models.Salesman
        {
            EmployeeNumber         = dto.EmployeeNumber,
            FirstName              = dto.FirstName,
            LastName               = dto.LastName,
            Level                  = dto.Level.Value,
            BirthDate              = dto.BirthDate.Value,
            HiredDate              = dto.HiredDate,
            Address                = dto.Address,
            City                   = dto.City,
            Phone                  = dto.Phone,
            SuperiorEmployeeNumber = dto.SuperiorEmployeeNumber
        });
        return dto;
    }
}

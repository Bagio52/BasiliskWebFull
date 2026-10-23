using System;

namespace Basilisk.Presentation.Web.ViewModels.Customer;

public class CustomerViewModel
{
    public long Id { get; set; }
    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
    public string Address { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime? DeleteDate { get; set; }

    public override string? ToString()
    {
        return  $"ID            : {Id} \n" +
                $"\tCompany Name: {CompanyName} \n" +
                $"\tContact Person: {ContactPerson} \n" +
                $"\tAddress     : {Address} \n" +
                $"\tCity        : {City} \n" +
                $"\tPhone       : {Phone} \n" +
                $"\tEmail       : {Email} \n" +
                $"\tDelete Date : {DeleteDate}";
    }
}

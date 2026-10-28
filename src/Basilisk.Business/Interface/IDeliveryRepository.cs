using System;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Interface;

public interface IDeliveryRepository
{
    List<Delivery> GetAllDeliveries();
    Delivery? GetDeliveryById(long id);
    Delivery AddDelivery(Delivery delivery);
    Delivery UpdateDelivery(Delivery delivery);
    bool DeleteDelivery(long id);
    IEnumerable<Delivery> SearchDeliveries(string companyName);
}

using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Repository;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly BasiliskTFContext _context;
    public DeliveryRepository(BasiliskTFContext context)
    {
        _context = context;
    }

    public List<Delivery> GetAllDeliveries()
    {
        return _context.Deliveries.ToList();
    }

    public Delivery? GetDeliveryById(long id)
    {
        return _context.Deliveries.Find(id);
    }

    public Delivery AddDelivery(Delivery delivery)
    {
        _context.Deliveries.Add(delivery);
        _context.SaveChanges();

        return delivery;
    }

    public Delivery UpdateDelivery(Delivery delivery)
    {
        _context.Deliveries.Update(delivery);
        _context.SaveChanges();

        return delivery;
    }

    public bool DeleteDelivery(long id)
    {
        var delivery = _context.Deliveries.Find(id);

        if (delivery == null)
        {
            return false;
        }

        _context.Deliveries.Remove(delivery);
        _context.SaveChanges();

        return true;
    }

    public IEnumerable<Delivery> SearchDeliveries(string companyName)
    {
        return _context.Deliveries
            .Where(d => d.CompanyName.Contains(companyName))
            .ToList();
    }
}

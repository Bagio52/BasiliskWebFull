using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels.Delivery;

namespace Basilisk.Presentation.Web.Services;

public class DeliveryService
{
    private readonly IDeliveryRepository _deliveryRepository;
    public DeliveryService(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }

    public DeliveryIndexViewModel GetDelivieries()
    {
        return new DeliveryIndexViewModel()
        {
            Deliveries = _deliveryRepository.GetAllDeliveries()
                .Select(d => new DeliveryViewModel()
                {
                    Id = d.Id,
                    CompanyName = d.CompanyName,
                    Phone = d.Phone,
                    Cost = d.Cost
                }).ToList()
        };
    }

    public void InsertDelivery(DeliveryViewModel vm)
    {
        _deliveryRepository.AddDelivery(new Delivery
        {
            CompanyName = vm.CompanyName,
            Phone = vm.Phone,
            Cost = vm.Cost
        });
    }

    public DeliveryViewModel? GetDeliveryById(long id)
    {
        var delivery = _deliveryRepository.GetDeliveryById(id);

        if (delivery == null)
        {
            return null;
        }

        return new DeliveryViewModel
        {
            Id = delivery.Id,
            CompanyName = delivery.CompanyName,
            Phone = delivery.Phone,
            Cost = delivery.Cost
        };
    }

    public bool UpdateDelivery(DeliveryViewModel vm)
    {
        var delivery = _deliveryRepository.GetDeliveryById(vm.Id);
        if (delivery == null)
        {
            return false;
        }

        delivery.CompanyName = vm.CompanyName;
        delivery.Phone = vm.Phone;
        delivery.Cost = vm.Cost;

        _deliveryRepository.UpdateDelivery(delivery);

        return true;
    }

    public bool DeleteDelivery(long id)
    {
        return _deliveryRepository.DeleteDelivery(id);
    }

    public DeliveryIndexViewModel SearchDeliveries(string companyName)
    {
        return new DeliveryIndexViewModel
        {
            Deliveries = _deliveryRepository
                .SearchDeliveries(companyName)
                .Select(d => new DeliveryViewModel
                {
                    Id = d.Id,
                    CompanyName = d.CompanyName,
                    Phone = d.Phone,
                    Cost = d.Cost
                }).ToList()
        };
    }

    public List<Delivery> GetAllDeliveries()
    {
        return _deliveryRepository.GetAllDeliveries();
    }    
}

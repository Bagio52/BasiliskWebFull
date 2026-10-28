using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels.Order;

namespace Basilisk.Presentation.Web.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public OrderIndexViewModel GetOrders()
    {
        return new OrderIndexViewModel()
        {
            Orders = _orderRepository.GetAllOrders()
                .Select(o => new OrderViewModel()
                {
                    InvoiceNumber = o.InvoiceNumber,
                    CustomerId = o.CustomerId,
                    CompanyName = o.Customer.CompanyName,
                    SalesEmployeeNumber = o.SalesEmployeeNumber,
                    FirstName = o.SalesEmployeeNumberNavigation.FirstName,
                    LastName = o.SalesEmployeeNumberNavigation.LastName ?? string.Empty,
                    OrderDate = o.OrderDate,
                    ShippedDate = o.ShippedDate,
                    DueDate = o.DueDate,
                    DeliveryId = o.DeliveryId,
                    DeliveryCompanyName = o.Delivery.CompanyName,
                    DeliveryCost = o.DeliveryCost,
                    DestinationAddress = o.DestinationAddress,
                    DestinationCity = o.DestinationCity,
                    DestinationPostalCode = o.DestinationPostalCode
                }).ToList()
        };
    }

    public Order InsertOrder(OrderViewModel vm)
    {
        return _orderRepository.AddOrder(new Order
        {
            InvoiceNumber = vm.InvoiceNumber,
            CustomerId = vm.CustomerId,
            SalesEmployeeNumber = vm.SalesEmployeeNumber,
            OrderDate = vm.OrderDate,
            ShippedDate = vm.ShippedDate,
            DueDate = vm.DueDate,
            DeliveryId = vm.DeliveryId,
            DeliveryCost = vm.DeliveryCost,
            DestinationAddress = vm.DestinationAddress,
            DestinationCity = vm.DestinationCity,
            DestinationPostalCode = vm.DestinationPostalCode
        });
    }

    public OrderViewModel? GetOrderById(string invoiceNumber)
    {
        var order = _orderRepository.GetOrderById(invoiceNumber);

        if (order == null)
        {
            return null;
        }

        return new OrderViewModel
        {
            InvoiceNumber = order.InvoiceNumber,
            CustomerId = order.CustomerId,
            SalesEmployeeNumber = order.SalesEmployeeNumber,
            OrderDate = order.OrderDate,
            ShippedDate = order.ShippedDate,
            DueDate = order.DueDate,
            DeliveryId = order.DeliveryId,
            DeliveryCost = order.DeliveryCost,
            DestinationAddress = order.DestinationAddress,
            DestinationCity = order.DestinationCity,
            DestinationPostalCode = order.DestinationPostalCode
        };
    }

    public bool UpdateOrder(OrderViewModel vm)
    {
        var order = _orderRepository.GetOrderById(vm.InvoiceNumber);

        if (order == null)
        {
            return false;
        }

        order.InvoiceNumber = vm.InvoiceNumber;
        order.CustomerId= vm.CustomerId;
        order.SalesEmployeeNumber = vm.SalesEmployeeNumber;
        order.OrderDate = vm.OrderDate;
        order.ShippedDate = vm.ShippedDate;
        order.DueDate = vm.DueDate;
        order.DeliveryId = vm.DeliveryId;
        order.DeliveryCost = vm.DeliveryCost;
        order.DestinationAddress = vm.DestinationAddress;
        order.DestinationCity = vm.DestinationCity;
        order.DestinationPostalCode = vm.DestinationPostalCode;

        _orderRepository.UpdateOrder(order);

        return true;
    }

    public bool DeleteOrder(string invoiceNumber)
    {
        return _orderRepository.DeleteOrder(invoiceNumber);
    }

    public OrderIndexViewModel SearchOrders(string invoiceNumber)
    {
        return new OrderIndexViewModel
        {
            Orders = _orderRepository
                .SearchOrders(invoiceNumber)
                .Select(o => new OrderViewModel
                {
                    InvoiceNumber = o.InvoiceNumber,
                    CustomerId = o.CustomerId,
                    CompanyName = o.Customer.CompanyName,
                    SalesEmployeeNumber = o.SalesEmployeeNumber,
                    FirstName = o.SalesEmployeeNumberNavigation.FirstName,
                    LastName = o.SalesEmployeeNumberNavigation.LastName ?? string.Empty,
                    OrderDate = o.OrderDate,
                    ShippedDate = o.ShippedDate,
                    DueDate = o.DueDate,
                    DeliveryId = o.DeliveryId,
                    DeliveryCompanyName = o.Delivery.CompanyName,
                    DeliveryCost = o.DeliveryCost,
                    DestinationAddress = o.DestinationAddress,
                    DestinationCity = o.DestinationCity,
                    DestinationPostalCode = o.DestinationPostalCode
                }).ToList()
        };
    }

    public List<Order> GetAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }
}

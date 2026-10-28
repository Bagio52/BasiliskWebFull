using System;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Interface;

public interface IOrderRepository
{
    List<Order> GetAllOrders();
    Order? GetOrderById(string invoiceNumber);
    Order AddOrder(Order order);
    Order UpdateOrder(Order order);
    bool DeleteOrder(string invoiceNumber);
    IEnumerable<Order> SearchOrders(string invoiceNumber);
}

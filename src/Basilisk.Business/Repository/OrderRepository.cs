using System;
using System.Transactions;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Basilisk.Business.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly BasiliskTFContext _context;

    public OrderRepository(BasiliskTFContext context)
    {
        _context = context;
    }

    public List<Order> GetAllOrders()
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.SalesEmployeeNumberNavigation)
            .Include(o => o.Delivery)
            .ToList();
    }

    public Order? GetOrderById(string invoiceNumber)
    {
        return _context.Orders.Find(invoiceNumber);
    }

    public Order AddOrder(Order order)
    {
        _context.Orders.Add(order);

        _context.SaveChanges();

        return order;
    }

    public Order UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();

        return order;
    }

    public bool DeleteOrder(string invoiceNumber)
    {
        var order = _context.Orders.Find(invoiceNumber);

        if (order == null)
        {
            return false;
        }

        _context.Orders.Remove(order);
        _context.SaveChanges();

        return true;
    }

    public IEnumerable<Order> SearchOrders(string invoiceNumber)
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.SalesEmployeeNumberNavigation)
            .Include(o => o.Delivery)
            .Where(o => o.InvoiceNumber.Contains(invoiceNumber))
            .ToList();
    }

}

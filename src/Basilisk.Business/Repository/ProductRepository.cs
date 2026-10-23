using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Repository;
 
public class ProductRepository : IProductRepository
{
     private readonly BasiliskTFContext _context;

     public ProductRepository(BasiliskTFContext context)
    {
        _context = context;
    }

    public List<Product> GetAllProduct()
    {
        return _context.Products.ToList();
    }

    public Product? GetProductById(long id)
    {
        return _context.Products.Find(id);
    }

    public Product AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public Product UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
        return product;
    }

    public bool DeleteProduct(long id)
    {
        var product = _context.Products.Find(id);

        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<Product> SearchProducts(string name)
    {
        return _context.Products
            .Where(p => p.Name.Contains(name))
            .ToList();
    }
}

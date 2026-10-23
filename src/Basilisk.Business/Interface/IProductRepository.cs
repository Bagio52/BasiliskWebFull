using System;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Interface;

public interface IProductRepository
{
    List<Product> GetAllProduct();
    Product? GetProductById(long id);
    Product AddProduct(Product product);
    Product UpdateProduct(Product product);
    bool DeleteProduct(long id);
    IEnumerable<Product> SearchProducts(string name);
}
 
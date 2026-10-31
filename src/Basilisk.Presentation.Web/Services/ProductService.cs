using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels;

namespace Basilisk.Presentation.Web.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
 
    public ProductIndexViewModel GetProducts(
        string? name = null,
        long? categoryId = null,
        long? supplierId = null)
    {
        var products = _productRepository.GetAllProduct()
            .Where(p => string.IsNullOrWhiteSpace(name) ||
                p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .Where(p => !categoryId.HasValue || p.CategoryId == categoryId.Value)
            .Where(p => !supplierId.HasValue || p.SupplierId == supplierId.Value)
            .Select(MapProduct)
            .ToList();

        return new ProductIndexViewModel()
        {
            Name = name,
            CategoryId = categoryId,
            SupplierId = supplierId,
            Products = products
        };
    }

    public void InsertProduct(ProductViewModel vm, string? imagePath = null)
    {
        _productRepository.AddProduct(new Product
        {
            Name = vm.Name,
            SupplierId = vm.SupplierId,
            CategoryId = vm.CategoryId,
            Description = vm.Description,
            Price = vm.Price,
            Stock = vm.Stock,
            OnOrder = vm.OnOrder,
            Discontinue = vm.Discontinued,
            ImagePath = imagePath
        });
    }

    public ProductViewModel? GetProductById(long id)
    {
        var product = _productRepository.GetProductById(id);

        if (product == null)
        {
            return null;
        }

        return new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            SupplierId = product.SupplierId,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            OnOrder = product.OnOrder,
            Discontinued = product.Discontinue,
            ImagePath = product.ImagePath
        };
    }

    public bool UpdateProduct(ProductViewModel vm)
    {
        var product = _productRepository.GetProductById(vm.Id);
        if (product == null)
        {
            return false;
        }

        product.Name = vm.Name;
        product.SupplierId = vm.SupplierId;
        product.CategoryId = vm.CategoryId;
        product.Description = vm.Description;
        product.Price = vm.Price;
        product.Stock = vm.Stock;
        product.OnOrder = vm.OnOrder;
        product.Discontinue = vm.Discontinued;
        product.ImagePath = vm.ImagePath;

        _productRepository.UpdateProduct(product);

        return true;
    }

    public bool DeleteProduct(long id)
    {
        return _productRepository.DeleteProduct(id);
    }

    public ProductIndexViewModel SearchProducts(string name)
    {
        return new ProductIndexViewModel
        {
            Products = _productRepository
                .SearchProducts(name)
                .Select(MapProduct)
                .ToList()
        };
    }

    private static ProductViewModel MapProduct(Product product)
    {
        return new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            SupplierId = product.SupplierId,
            SupplierName = product.Supplier?.CompanyName,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            OnOrder = product.OnOrder,
            Discontinued = product.Discontinue,
            ImagePath = product.ImagePath
        };
    }
}

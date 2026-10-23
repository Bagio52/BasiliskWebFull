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
 
    public ProductIndexViewModel GetProducts()
    {
        return new ProductIndexViewModel()
        {
            Products = _productRepository.GetAllProduct()
            .Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                SupplierId = p.SupplierId,
                CategoryId = p.CategoryId,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                OnOrder = p.OnOrder,
                Discontinued = p.Discontinue,
                ImagePath = p.ImagePath
            }).ToList()
        };
    }

    public void InsertProduct(ProductViewModel vm)
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
            ImagePath = null
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
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SupplierId = p.SupplierId,
                    CategoryId = p.CategoryId,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    OnOrder = p.OnOrder,
                    Discontinued = p.Discontinue,
                    ImagePath = p.ImagePath
                })
                .ToList()
        };
    }
}

using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Basilisk.Presentation.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _service;
        private readonly CategoryService _categoryService;
        private readonly SupplierService _supplierService;

        public ProductController(ILogger<ProductController> logger, ProductService service, CategoryService categoryService, SupplierService supplierService)
        {
            _logger = logger;
            _service = service;
            _categoryService = categoryService;
            _supplierService = supplierService;
        }

        [HttpGet]
        public IActionResult Index(
            string? name,
            long? categoryId,
            long? supplierId
        )
        {
            var categories = _categoryService.GetAllCategories();
            var suppliers = _supplierService.GetAllSuppliers();

            var model = new ProductIndexViewModel
            {
                CategoryId = categoryId,
                SupplierId = supplierId,

                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = c.Id == categoryId
                }).ToList(),

                Suppliers = suppliers.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.CompanyName,
                    Selected = s.Id == supplierId
                }).ToList()
            };

            // Ambil daftar produk di sini, menggunakan filter
            // name, categoryId, dan supplierId.

            return View(model);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            var categories = _categoryService.GetAllCategories();

            var model = new ProductInsertViewModel
            {
                Products = new ProductViewModel(),
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Insert(ProductInsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = _categoryService.GetAllCategories();
                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(model);
            }

            // Simpan produk baru menggunakan ProductService
            // _service.InsertProduct(model.Product);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _service.GetProductById(id);

            if (model == null)
            {
                return NotFound();
            }

            var categories = _categoryService.GetAllCategories();
            
            return View(new ProductInsertViewModel
            {
                Products = model,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = c.Id == model.CategoryId
                }).ToList()
            });
        }

        [HttpPost]
        public IActionResult Edit(ProductInsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = _categoryService.GetAllCategories();
                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = c.Id == model.Products.CategoryId
                }).ToList();

                return View(model);
            }

            var updated = _service.UpdateProduct(model.Products);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var deleted = _service.DeleteProduct(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}


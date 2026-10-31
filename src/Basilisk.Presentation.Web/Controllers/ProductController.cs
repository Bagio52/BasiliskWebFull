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
        private readonly IWebHostEnvironment _environment;

        public ProductController(
            ILogger<ProductController> logger,
            ProductService service,
            CategoryService categoryService,
            SupplierService supplierService,
            IWebHostEnvironment environment)
        {
            _logger = logger;
            _service = service;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index(
            string? name,
            long? categoryId,
            long? supplierId
        )
        {
            name = name?.Trim();
            var categories = _categoryService.GetAllCategories();
            var suppliers = _supplierService.GetAllSuppliers();
            var model = _service.GetProducts(name, categoryId, supplierId);

            model.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == categoryId
            }).ToList();
            model.Suppliers = suppliers
                .Select(supplier => new SelectListItem
                {
                    Value = supplier.Id.ToString(),
                    Text = supplier.CompanyName,
                    Selected = supplier.Id == supplierId
                })
                .ToList();

            // Ambil daftar produk di sini, menggunakan filter
            // name, categoryId, dan supplierId.

            return View(model);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            var model = new ProductInsertViewModel
            {
                Products = new ProductViewModel()
            };

            PopulateProductOptions(model);
            return View(model);
        }

        [HttpPost]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> Insert(ProductInsertViewModel model)
        {
            string? imagePath = null;
            if (model.Image != null && model.Image.Length > 0)
            {
                var extension = Path.GetExtension(model.Image.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(
                        nameof(model.Image),
                        "Format gambar harus JPG, JPEG, PNG, GIF, atau WEBP.");
                }
            }

            if (!ModelState.IsValid)
            {
                PopulateProductOptions(model);
                return View(model);
            }

            if (model.Image != null && model.Image.Length > 0)
            {
                var extension = Path.GetExtension(model.Image.FileName).ToLowerInvariant();
                var fileName = $"{Guid.NewGuid():N}{extension}";
                var uploadDirectory = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "products");
                Directory.CreateDirectory(uploadDirectory);

                var filePath = Path.Combine(uploadDirectory, fileName);
                await using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await model.Image.CopyToAsync(stream);
                }

                imagePath = $"/uploads/products/{fileName}";
            }

            _service.InsertProduct(model.Products, imagePath);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var product = _service.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductInsertViewModel
            {
                Products = product
            };
            PopulateProductOptions(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductInsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateProductOptions(model);
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

        private void PopulateProductOptions(ProductInsertViewModel model)
        {
            model.Categories = _categoryService.GetAllCategories()
                .Select(category => new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name,
                    Selected = category.Id == model.Products.CategoryId
                })
                .ToList();

            model.Suppliers = _supplierService.GetAllSuppliers()
                .Select(supplier => new SelectListItem
                {
                    Value = supplier.Id.ToString(),
                    Text = supplier.CompanyName,
                    Selected = supplier.Id == model.Products.SupplierId
                })
                .ToList();
        }
    }
}

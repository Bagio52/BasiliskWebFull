using System.Reflection.Metadata.Ecma335;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Basilisk.Presentation.Web.Controllers
{
    [Route("[controller]/[action]")]
    // [ApiController]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly CategoryService _service;

        public CategoryController(ILogger<CategoryController> logger, CategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(string? searchName)
        {
            var model = string.IsNullOrWhiteSpace(searchName)
                ? _service.GetCategories()
                : _service.SearchCategories(searchName);

            ViewBag.SearchName = searchName;

            return View(model);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View(new CategoryInsertViewModel
            {
                Category = new CategoryViewModel()
            });
        }

        [HttpPost]
        public IActionResult Insert(CategoryInsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _service.InsertCategory(model.Category);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _service.GetCategoryById(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updated = _service.UpdateCategory(model);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            var deleted = _service.DeleteCategory(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}

using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Api.Categories;
[Route("api/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetCategoryList()
    {
        return Ok(_service.GetCategories());
    }

    [HttpPost]
    public IActionResult InsertCategory(CategoryDto dto)
    {
        return Ok(_service.InsertCategory(dto));
    }
}

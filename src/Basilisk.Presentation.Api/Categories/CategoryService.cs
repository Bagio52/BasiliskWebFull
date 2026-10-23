using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;

namespace Basilisk.Presentation.Api.Categories;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public List<CategoryDto> GetCategories()
    {
        return _categoryRepository.GetAllCategories()
            .Select(c => new CategoryDto()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
    }

    public CategoryDto InsertCategory(CategoryDto dto)
    {
        var newCategory = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };
        var InsertCategory = _categoryRepository.AddCategory(newCategory);
        return new CategoryDto
        {
            Id = InsertCategory.Id,
            Name = InsertCategory.Name,
            Description = InsertCategory.Description
        };
    }
}

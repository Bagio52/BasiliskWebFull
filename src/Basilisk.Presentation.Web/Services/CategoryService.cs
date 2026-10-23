using System;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels;

namespace Basilisk.Presentation.Web.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public CategoryIndexViewModel GetCategories()
    {
        return new CategoryIndexViewModel()
        {
            Categories = _categoryRepository.GetAllCategories()
            .Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList()
        };
    }

    public void InsertCategory(CategoryViewModel vm)
    {
        _categoryRepository.AddCategory(new Category
        {
            Name = vm.Name,
            Description = vm.Description
        });
    }

    public CategoryViewModel? GetCategoryById(long id)
    {
        var category = _categoryRepository.GetCategoryById(id);

        if (category == null)
        {
            return null;
        }

        return new CategoryViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };

    }

    public bool UpdateCategory(CategoryViewModel vm)
    {
        var category = _categoryRepository.GetCategoryById(vm.Id);
        if (category == null)
        {
            return false;
        }

        category.Name = vm.Name;
        category.Description = vm.Description;

        _categoryRepository.UpdateCategory(category);

        return true; 

    }
    public bool DeleteCategory(long id)
    {
        return _categoryRepository.DeleteCategory(id);
    }

    public CategoryIndexViewModel SearchCategories(string name)
    {
        return new CategoryIndexViewModel
        {
            Categories = _categoryRepository
                .SearchCategories(name)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList()
        };
    }

    public List<Category> GetAllCategories()
    {
        return _categoryRepository.GetAllCategories();
    }
}

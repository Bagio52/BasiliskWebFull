using System;
using System.Linq;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly BasiliskTFContext _context;

    public CategoryRepository(BasiliskTFContext context)
    {
        _context = context;
    }

    public List<Category> GetAllCategories()
    {
        return _context.Categories.ToList();
        //.Include(Category => category.product);
    }

    public Category? GetCategoryById(long id)
    {
        return _context.Categories.Find(id);
    }

    public Category AddCategory(Category category)
    {
        _context.Categories.Add(category);

        _context.SaveChanges();

        return category;
    }

    public Category UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();

        return category;
    }

    public bool DeleteCategory(long id)
    {
        var category = _context.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();

        return true;
    }

    public IEnumerable<Category> SearchCategories(string name)
    {
        return _context.Categories
            .Where(c => c.Name.Contains(name))
            .ToList();
    }

}

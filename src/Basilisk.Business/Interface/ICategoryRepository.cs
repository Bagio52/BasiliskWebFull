using System;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Interface;

public interface ICategoryRepository
{
    List<Category> GetAllCategories();
    Category? GetCategoryById(long id);
    Category AddCategory(Category category);
    Category UpdateCategory(Category category);
    bool DeleteCategory(long id);
    IEnumerable<Category> SearchCategories(string name);
}

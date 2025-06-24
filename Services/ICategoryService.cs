using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        void AddCategory(Category c);
        void DeleteCategory(Category c);
        void UpdateCategory(Category c);
        Category GetCategoryById(short id);
        List<Category> GetCategoryByName(string name);
    }
}

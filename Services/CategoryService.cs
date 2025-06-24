using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        public List<Category> GetCategories() => CategoryDAO.Instance.GetCategories();
        public void AddCategory(Category c) => CategoryDAO.Instance.AddCategory(c);
        public void DeleteCategory(Category c) => CategoryDAO.Instance.DeleteCategory(c);
        public void UpdateCategory(Category c) => CategoryDAO.Instance.UpdateCategory(c);
        public Category GetCategoryById(short id) => CategoryDAO.Instance.GetCategoryById(id);
        public List<Category> GetCategoryByName(string name) => CategoryDAO.Instance.GetCategoryByName(name);
    }
}

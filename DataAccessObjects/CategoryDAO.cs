using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        private static readonly object instanceLock = new object();
        private readonly FunewsManagementContext _context;
        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }
        public CategoryDAO()
        {
            _context = new FunewsManagementContext();
        }
        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();
            try
            {
                list = _context.Categories.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public void AddCategory(Category c)
        {
            try
            {
                _context.Categories.Add(c);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteCategory(Category c)
        {
            try
            {
                _context.Categories.Remove(c);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateCategory(Category c)
        {
            try
            {
                _context.Entry<Category>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Category GetCategoryById(short id)
        {
            try
            {
                return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Category> GetCategoryByName(string name)
        {
            List<Category> list = new List<Category>();
            try
            {
                list = _context.Categories.Where(c => c.CategoryName.Equals(name)).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}

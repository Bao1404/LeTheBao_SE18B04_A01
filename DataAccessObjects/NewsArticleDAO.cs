using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class NewsArticleDAO
    {
        private static NewsArticleDAO instance;
        private static readonly object instanceLock = new object();
        private readonly FunewsManagementContext _context;
        public static NewsArticleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NewsArticleDAO();
                }
                return instance;
            }
        }
        public NewsArticleDAO()
        {
            _context = new FunewsManagementContext();
        }
        public List<NewsArticle> GetAll()
        {
            List<NewsArticle> list = new List<NewsArticle>();
            try
            {
                list = _context.NewsArticles.Include(n => n.Category).Include(n => n.CreatedBy).Include(n => n.Tags).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public NewsArticle GetArticleById(string id)
        {
            try
            {
                return _context.NewsArticles.FirstOrDefault(n => n.NewsArticleId.Equals(id));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<NewsArticle> GetArticleByCategory(string categoryName)
        {
            List<NewsArticle> list = new List<NewsArticle>();
            try
            {
                list = _context.NewsArticles.Where(n => n.Category.CategoryName.Equals(categoryName)).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public List<NewsArticle> GetArticleByUserId(short userId)
        {
            List<NewsArticle> list = new List<NewsArticle>();
            try
            {
                list = _context.NewsArticles.Where(n => n.CreatedById == userId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public void AddNewsArticle(NewsArticle article, List<int> tagIds)
        {
            try
            {
                var tags = _context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();
                article.Tags = tags;
                _context.NewsArticles.Add(article);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateNewsArticle(NewsArticle article, List<int> tagIds)
        {
            try
            {
                var tags = _context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();
                article.Tags = tags;
                _context.Entry<NewsArticle>(article).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteNewsArticle(NewsArticle article)
        {
            try
            {
                _context.Entry(article).Collection(a => a.Tags).Load();

                article.Tags.Clear();
                _context.SaveChanges();

                _context.NewsArticles.Remove(article);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<NewsArticle> SearchArticle(string input)
        {
            bool status = false;
            if (input.ToLower().Equals("PUBLISHED"))
            {
                status = true;
            }
            List<NewsArticle> list = new List<NewsArticle>();
            try
            {
                list = _context.NewsArticles.Where(n => n.NewsTitle.Equals(input) || n.Category.CategoryName.Equals(input) || n.Tags.Any(t => t.TagName.Equals(input)) || n.NewsStatus == status).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public List<NewsArticle> GetReportedArticles(DateOnly start, DateOnly end)
        {
            List<NewsArticle> list = new List<NewsArticle>();
            try
            {
                DateTime startDateTime = start.ToDateTime(TimeOnly.MinValue); 
                DateTime endDateTime = end.ToDateTime(TimeOnly.MaxValue);
                list = _context.NewsArticles.Include(n => n.Category).Include(n => n.Tags).Where(n => n.CreatedDate >= startDateTime && n.CreatedDate <= endDateTime).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}

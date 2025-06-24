using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NewsArticleService : INewsArticleService
    {
        public List<NewsArticle> GetAll() => NewsArticleDAO.Instance.GetAll();
        public NewsArticle GetArticleById(string id) => NewsArticleDAO.Instance.GetArticleById(id);
        public List<NewsArticle> GetArticleByCategory(string categoryName) => NewsArticleDAO.Instance.GetArticleByCategory(categoryName);
        public List<NewsArticle> GetArticleByUserId(short userId) => NewsArticleDAO.Instance.GetArticleByUserId(userId);
        public void AddNewsArticle(NewsArticle article, List<int> tagIds) => NewsArticleDAO.Instance.AddNewsArticle(article, tagIds);
        public void DeleteNewsArticle(NewsArticle a) => NewsArticleDAO.Instance.DeleteNewsArticle(a);
        public void UpdateNewsArticle(NewsArticle a, List<int> tagIds) => NewsArticleDAO.Instance.UpdateNewsArticle(a, tagIds);
        public List<NewsArticle> SearchArticle(string input) => NewsArticleDAO.Instance.SearchArticle(input);
        public List<NewsArticle> GetReportedArticles(DateOnly start, DateOnly end) => NewsArticleDAO.Instance.GetReportedArticles(start, end);
    }
}

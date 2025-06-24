using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface INewsArticleService
    {
        List<NewsArticle> GetAll();
        NewsArticle GetArticleById(string id);
        List<NewsArticle> GetArticleByCategory(string categoryName);
        public List<NewsArticle> GetArticleByUserId(short userId);
        void AddNewsArticle(NewsArticle article, List<int> tagIds);
        void DeleteNewsArticle(NewsArticle article);
        void UpdateNewsArticle(NewsArticle article, List<int> tagIds);
        List<NewsArticle> SearchArticle(string input);
        List<NewsArticle> GetReportedArticles(DateOnly start, DateOnly end);
    }
}

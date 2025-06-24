using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LeTheBaoMVC.Models;
using Services;
using BusinessObjects;

namespace LeTheBaoMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INewsArticleService _newsArticleService;
    private readonly ITagService _tagService;

    public HomeController(ILogger<HomeController> logger, INewsArticleService newsArticleService, ITagService tagService)
    {
        _logger = logger;
        _newsArticleService = newsArticleService;
        _tagService = tagService;
    }

    public IActionResult Index()
    {
        var user = HttpContext.Session.GetString("UserId");
        if (user == null)
        {
            return RedirectToAction("Index", "Login");
        }
        var articles = _newsArticleService.GetAll();
        if(articles != null && articles.Count > 0)
        {
            ViewBag.ArticleList = articles;
        }
        
        ViewBag.ArticleList = _newsArticleService.GetAll();

        var tags = _tagService.GetAllTags();
        if (tags != null)
        {
            ViewBag.TagList = tags;
        }
        return View();
    }

    public IActionResult ArticleDetail(string id)
    {
        var user = HttpContext.Session.GetString("UserId");
        if (user == null)
        {
            return RedirectToAction("Index", "Login");
        }
        var article = _newsArticleService.GetArticleById(id);
        if (article != null)
        {
            ViewBag.Article = article;
        }
        List<NewsArticle> relatedArticle = _newsArticleService.GetArticleByCategory(article.Category.CategoryName);
        if(relatedArticle != null)
        {
            ViewBag.RelatedArticle = relatedArticle;
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace LeTheBaoMVC.Controllers
{
    public class StaffController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly INewsArticleService _newsArticleService;
        private readonly IAccountService _accountService;
        private readonly ITagService _tagService;
        public StaffController(ICategoryService categoryService, INewsArticleService newsArticleService, IAccountService accountService, ITagService tagService)
        {
            _categoryService = categoryService;
            _newsArticleService = newsArticleService;
            _accountService = accountService;
            _tagService = tagService;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                var user = _accountService.GetAccountByUserId(short.Parse(userId));
                ViewBag.User = user;
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

                var categories = _categoryService.GetCategories();
            if (categories != null)
            {
                ViewBag.ListCategory = categories;
            }

            var newsArticles = _newsArticleService.GetAll();
            if (newsArticles != null)
            {
                ViewBag.ListArticle = newsArticles;
                foreach (var article in newsArticles)
                {
                    ViewBag.SelectedTagIds = article.Tags.Select(t => t.TagId).ToList();
                }
            }
            var articleByUser = _newsArticleService.GetArticleByUserId(short.Parse(userId));
            if (articleByUser != null)
            {
                ViewBag.ListByUser = articleByUser;
            }
            var tags = _tagService.GetAllTags();
            if (tags != null)
            {
                ViewBag.ListTag = tags;
            }
            return View();
        }
        [HttpPost("/Category")]
        public IActionResult AddCategory()
        {
            var CategoryName = Request.Form["categoryName"];
            var CategoryDescription = Request.Form["categoryDescription"];
            if (!string.IsNullOrEmpty(CategoryName) && !string.IsNullOrEmpty(CategoryDescription))
            {
                var category = new Category
                {
                    CategoryName = CategoryName,
                    CategoryDesciption = CategoryDescription,
                    IsActive = true
                };
                _categoryService.AddCategory(category);
            }
            return RedirectToAction("Index", "Staff");
        }
        [HttpPost("/Category/Update")]
        public IActionResult UpdateCategory()
        {
            var CategoryId = Request.Form["categoryId"];
            var CategoryName = Request.Form["categoryName"];
            var CategoryDescription = Request.Form["categoryDescription"];
            if (!string.IsNullOrEmpty(CategoryName) && !string.IsNullOrEmpty(CategoryDescription) && !string.IsNullOrEmpty(CategoryId))
            {
                var category = _categoryService.GetCategoryById(short.Parse(CategoryId));
                if (category != null)
                {
                    category.CategoryName = CategoryName;
                    category.CategoryDesciption = CategoryDescription;
                    _categoryService.UpdateCategory(category);
                }
            }
            return RedirectToAction("Index", "Staff");
        }
        [HttpPost("/Category/Delete")]
        public IActionResult DeleteCategory()
        {
            var CategoryId = Request.Form["categoryId"];
            if (!string.IsNullOrEmpty(CategoryId))
            {
                var category = _categoryService.GetCategoryById(short.Parse(CategoryId));
                if (category != null)
                {
                    _categoryService.DeleteCategory(category);
                }
            }
            return RedirectToAction("Index", "Staff");
        }
        [HttpPost("/Article")]
        public IActionResult AddArticle()
        {
            var id = Request.Form["id"];
            var Title = Request.Form["title"];
            var Headline = Request.Form["headline"];
            var Content = Request.Form["content"];
            var CategoryId = Request.Form["categoryId"];
            var UserId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Content) || string.IsNullOrEmpty(CategoryId) || string.IsNullOrEmpty(UserId))
            {
                ViewBag.ErrorMessage = "Please fill in all required fields.";
                return View();
            }

            var user = _accountService.GetAccountByUserId(short.Parse(UserId));
            var category = _categoryService.GetCategoryById(short.Parse(CategoryId));
            if (category == null)
            {
                return View();
            }

            var Tags = Request.Form["tag"];
            bool newsStatus = Request.Form["status"] == "1";

            List<int> tagList = new List<int>();
            if (Tags.Count > 0)
            {
                foreach (var tagId in Tags)
                {
                    tagList.Add(int.Parse(tagId));
                }
            }

            var article = new NewsArticle
            {
                NewsArticleId = id,
                NewsTitle = Title,
                NewsContent = Content,
                CategoryId = category.CategoryId,
                CreatedById = user.AccountId,
                CreatedDate = DateTime.Now,
                Headline = Headline,
                NewsStatus = newsStatus
            };

            _newsArticleService.AddNewsArticle(article, tagList);

            return RedirectToAction("Index", "Staff");
        }
        [HttpPost("/Article/Update")]
        public IActionResult UpdateArticle()
        {
            var id = Request.Form["id"];
            var Title = Request.Form["title"];
            var Headline = Request.Form["headline"];
            var Content = Request.Form["content"];
            var CategoryId = Request.Form["categoryId"];
            var UserId = HttpContext.Session.GetString("UserId");
            var article = _newsArticleService.GetArticleById(id);
            if (article != null)
            {
                if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Content) || string.IsNullOrEmpty(CategoryId) || string.IsNullOrEmpty(UserId))
                {
                    ViewBag.ErrorMessage = "Please fill in all required fields.";
                    return View();
                }
                var user = _accountService.GetAccountByUserId(short.Parse(UserId));
                var category = _categoryService.GetCategoryById(short.Parse(CategoryId));
                if (category == null)
                {
                    return View();
                }
                var Tags = Request.Form["tag"];
                bool newsStatus = Request.Form["status"] == "1";
                List<int> tagList = new List<int>();
                if (Tags.Count > 0)
                {
                    foreach (var tagId in Tags)
                    {
                        tagList.Add(int.Parse(tagId));
                    }
                }
                article.NewsTitle = Title;
                article.NewsContent = Content;
                article.CategoryId = category.CategoryId;
                article.UpdatedById = user.AccountId;
                article.ModifiedDate = DateTime.Now;
                article.Headline = Headline;
                article.NewsStatus = newsStatus;
                _newsArticleService.UpdateNewsArticle(article, tagList);
            }
            return RedirectToAction("Index", "Staff");
        }
        [HttpPost("/Article/Delete")]
        public IActionResult DeleteArticle()
        {
            var articleId = Request.Form["id"];
            if (!string.IsNullOrEmpty(articleId))
            {
                var article = _newsArticleService.GetArticleById(articleId);
                if (article != null)
                {
                    _newsArticleService.DeleteNewsArticle(article);
                }
            }
            return RedirectToAction("Index", "Staff");
        }
        [HttpPost("/Account/Update")]
        public IActionResult UpdateAccount()
        {
            var accountId = Request.Form["accountId"];
            var email = Request.Form["email"];
            var fullName = Request.Form["fullName"];
            var password = Request.Form["password"];
            var user = _accountService.GetAccountByUserId(short.Parse(accountId));
            if (user != null)
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(password))
                {
                    ViewBag.ErrorMessage = "Please fill in all required fields.";
                    return View();
                }
                user.AccountEmail = email;
                user.AccountName = fullName;
                user.AccountPassword = password;
                _accountService.UpdateAccount(user);
            }
            return RedirectToAction("Index", "Staff");
        }
        [HttpGet("/Search/Category")]
        public IActionResult SearchCategory()
        {
            var category = Request.Query["category"];
            if (!string.IsNullOrEmpty(category))
            {
                var cat = _categoryService.GetCategoryByName(category);
                ViewBag.ListCategory = cat;
            }
            else
            {
                ViewBag.ListCategory = _categoryService.GetCategories();
            }

            return PartialView("Partials/_CategoryPartial");
        }
        [HttpGet("/Search/Article")]
        public IActionResult SearchArticle()
        {
            var input = Request.Query["article"];
            if (!string.IsNullOrEmpty(input))
            {
                var articles = _newsArticleService.SearchArticle(input);
                ViewBag.ListArticle = articles;
            }
            else
            {
                ViewBag.ListArticle = _newsArticleService.GetAll();
            }
            var tags = _tagService.GetAllTags();
            if (tags != null)
            {
                ViewBag.ListTag = tags;
            }
            var categories = _categoryService.GetCategories();
            if (categories != null)
            {
                ViewBag.ListCategory = categories;
            }
            return PartialView("Partials/_ArticlePartial");
        }
    }
}

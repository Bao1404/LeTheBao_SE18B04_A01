using Microsoft.AspNetCore.Mvc;
using Services;

namespace LeTheBaoMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly INewsArticleService _newsArticleService;
        public AdminController(IAccountService accountService, INewsArticleService newsArticleService)
        {
            _accountService = accountService;
            _newsArticleService = newsArticleService;
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("Role");
            if (!user.Equals("Admin"))
            {
                return RedirectToAction("Index", "Login");
            }
            var accountList = _accountService.GetAllAccount();
            if (accountList != null)
            {
                ViewBag.AccountList = accountList;
            }
            return View();
        }
        [HttpPost("/User/Role")]
        public IActionResult UpdateRole()
        {
            var userId = Request.Form["accountId"];
            var role = Request.Form["role"];
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(role))
            {
                var account = _accountService.GetAccountByUserId(short.Parse(userId));
                if (account != null)
                {
                    account.AccountRole = int.Parse(role);
                    _accountService.UpdateAccount(account);
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        [HttpGet("/Search/User")]
        public IActionResult SearchUser()
        {
            var search = Request.Query["input"];
            if (!string.IsNullOrEmpty(search))
            {
                var accountList = _accountService.SearchAccount(search);
                if (accountList != null)
                {
                    ViewBag.AccountList = accountList;
                }
            }
            return PartialView("Partials/_UserPartial");
        }
        [HttpGet("/Article/Report")]
        public IActionResult ReportArticle()
        {
            var startDate = Request.Query["start"];
            var endDate = Request.Query["end"];
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                var list = _newsArticleService.GetReportedArticles(DateOnly.Parse(startDate), DateOnly.Parse(endDate));
                if(list != null && list.Count > 0)
                {
                    ViewBag.ListReport = list;
                }
            }
            return PartialView("Partials/_ReportPartial");
        }
    }
}

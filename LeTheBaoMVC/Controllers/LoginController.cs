using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace LeTheBaoMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        public LoginController(IAccountService accountService, IConfiguration configuration)
        {
            _configuration = configuration;
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/Login")]
        public IActionResult Login(SystemAccount systemAccount)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", systemAccount);
            }

            var adminEmail = _configuration["DefaultAdminAccount:Email"];
            var adminPassword = _configuration["DefaultAdminAccount:Password"];

            if (systemAccount.AccountEmail.Equals(adminEmail) && systemAccount.AccountPassword.Equals(adminPassword))
            {
                HttpContext.Session.SetString("Role", "Admin");
                return RedirectToAction("Index", "Admin");
            }

            var email = systemAccount.AccountEmail;
            var password = systemAccount.AccountPassword;
            var account = _accountService.GetAccountByEmail(email, password);

            if (account != null)
            {
                HttpContext.Session.SetString("UserId", account.AccountId.ToString());

                if (account.AccountRole == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (account.AccountRole == 2)
                {
                    return RedirectToAction("Index", "Staff");
                }
            }

            ViewBag.ErrorMessage = "Wrong email or password";
            return View("Index", systemAccount);
        }
    }
}

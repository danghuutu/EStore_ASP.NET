using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class LoginController : Controller
    {
        string access;
        string admin;

        IMemberRepository memberRepository = null;
        public LoginController() => memberRepository = new MemberRepository();
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {

            // Kiểm tra thông tin đăng nhập
            if (IsValidUser(email, password))
            {
                // Lưu thông tin đăng nhập vào cookies
                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30),
                    IsEssential = true
                };
                Response.Cookies.Append("Email", email, options);
                Response.Cookies.Append("Password", password, options);
                Response.Cookies.Append("Access", access, options);
                Response.Cookies.Append("Admin", admin, options);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password";
                return View();
            }
        }
        private bool IsValidUser(string email, string password)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Retrieve the email and password values from the configuration object
            var Email = config["DefaultAccount:Email"];
            var Password = config["DefaultAccount:Password"];
            // Kiểm tra thông tin đăng nhập
            if (email == Email && password == Password)
            {
                access = "true";
                admin = "true";
                return true;
            }
            foreach (var i in memberRepository.GetMember())
            {
                if (i.Email == email && i.Password == password)
                {
                    access = "true";
                    admin = "false";
                    return true;
                }
            }
            return false;
        }
    }
}

using LaptopStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.Admins
                .FirstOrDefault(x =>
                    x.UserName == username &&
                    x.Password == password);

            if (admin != null)
            {
                HttpContext.Session.SetInt32(
                 "AdminId",
                admin.Id);

                HttpContext.Session.SetString(
                 "AdminName",
                 admin.UserName);

                return RedirectToAction(
                 "Index",
                 "Dashboard",
                 new { area = "Admin" });


            }
            ViewBag.Error = "Sai tài khoản hoặc mật khẩu";

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminId");
            HttpContext.Session.Remove("AdminName");

            return RedirectToAction("Login");
        }
    }
}
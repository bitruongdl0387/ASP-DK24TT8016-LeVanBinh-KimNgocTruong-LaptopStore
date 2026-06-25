using LaptopStore.Data;
using LaptopStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string phone)
        {
            var customer = _context.Customers
                .FirstOrDefault(x =>
                    x.Email == email &&
                    x.Phone == phone);

            if (customer != null)
            {
                HttpContext.Session.SetInt32(
                    "CustomerId",
                    customer.Id);

                HttpContext.Session.SetString(
                    "CustomerName",
                    customer.FullName ?? "");

                return RedirectToAction(
                    "Index",
                    "Home");
            }

            ViewBag.Error = "Email hoặc số điện thoại không đúng";

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Index",
                "Home");
        }
    }
}
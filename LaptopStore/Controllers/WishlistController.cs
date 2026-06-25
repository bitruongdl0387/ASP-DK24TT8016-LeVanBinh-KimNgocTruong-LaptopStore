using LaptopStore.Data;
using LaptopStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Add(int productId)
        {
            var customerId =
                HttpContext.Session.GetInt32("CustomerId");

            if (customerId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var wishlist = new Wishlist
            {
                CustomerId = customerId.Value,
                ProductId = productId
            };

            _context.Wishlists.Add(wishlist);
            _context.SaveChanges();

            return RedirectToAction(
                "Index",
                "Home");
        }

        public IActionResult Index()
        {
            var customerId =
                HttpContext.Session.GetInt32("CustomerId");

            if (customerId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var productIds = _context.Wishlists
                .Where(x => x.CustomerId == customerId.Value)
                .Select(x => x.ProductId)
                .ToList();

            var products = _context.Products
                .Where(x => productIds.Contains(x.Id))
                .ToList();

            return View(products);
        }
    }
}
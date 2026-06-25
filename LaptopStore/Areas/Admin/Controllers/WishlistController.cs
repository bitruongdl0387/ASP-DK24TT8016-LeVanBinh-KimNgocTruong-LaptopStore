using LaptopStore.Data;
using LaptopStore.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var wishlists = _context.Wishlists
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(wishlists);
        }
    }
}
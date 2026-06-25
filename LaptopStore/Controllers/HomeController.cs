using LaptopStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Khach hang loc san pham theo hang
        public async Task<IActionResult> Index(
            string searchString,
            int? brandId)
        {
            ViewBag.SelectedBrandId =brandId;
            ViewBag.Brands =
                await _context.Brands.ToListAsync();

            var products = _context.Products
                .Where(x => x.IsActive);

            if (brandId.HasValue)
            {
                products = products.Where(x =>
                    x.BrandId == brandId);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(x =>
                    x.Name.Contains(searchString) ||
                    x.Cpu.Contains(searchString) ||
                    x.Ram.Contains(searchString));
            }

            return View(await products.ToListAsync());
        }
    }
}
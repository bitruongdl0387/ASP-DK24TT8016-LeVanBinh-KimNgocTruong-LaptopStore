using LaptopStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.RelatedProducts =
        await _context.Products
            .Where(x => x.BrandId == product.BrandId
                     && x.Id != product.Id)
            .Take(4)
            .ToListAsync();

            return View(product);
        }
    }

}
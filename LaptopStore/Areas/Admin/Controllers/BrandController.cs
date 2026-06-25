using LaptopStore.Data;
using LaptopStore.Filters;
using LaptopStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var brands = _context.Brands.ToList();

            return View(brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
                return NotFound();

            return View(brand);
        }

        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            _context.Brands.Update(brand);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
                return NotFound();

            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
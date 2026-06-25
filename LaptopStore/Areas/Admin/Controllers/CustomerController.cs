using LaptopStore.Data;
using LaptopStore.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();

            return View(customers);
        }
    }
}
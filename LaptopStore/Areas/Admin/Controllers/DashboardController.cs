using LaptopStore.Data;
using Microsoft.AspNetCore.Mvc;
using LaptopStore.Filters;

namespace LaptopStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalProducts =
                _context.Products.Count();

            ViewBag.TotalOrders =
                _context.Orders.Count();

            ViewBag.TotalCustomers =
                _context.Customers.Count();

            ViewBag.TotalRevenue =
                _context.Orders.Sum(x => x.TotalAmount);

            ViewBag.PendingOrders =
                _context.Orders.Count(x => x.Status == "Pending");

            ViewBag.CompletedOrders =
                _context.Orders.Count(x => x.Status == "Completed");

            ViewBag.CancelledOrders =
                _context.Orders.Count(x => x.Status == "Cancelled");

            ViewBag.RecentOrders =
                _context.Orders
                    .OrderByDescending(x => x.Id)
                    .Take(5)
                    .ToList();

            return View();
        }
    }
}
using LaptopStore.Data;
using LaptopStore.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders
               .Include(x => x.Customer)
               .OrderByDescending(x => x.Id)
                .ToList();
            return View(orders);
         }
        public IActionResult Detail(int id)
        {
            var order = _context.Orders
                .FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDetails = _context.OrderDetails
                .Where(x => x.OrderId == id)
                .ToList();

            ViewBag.Order = order;

            return View(orderDetails);
        }
        public IActionResult UpdateStatus(int id, string status)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
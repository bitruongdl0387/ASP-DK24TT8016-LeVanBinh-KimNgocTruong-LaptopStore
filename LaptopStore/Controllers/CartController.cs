using LaptopStore.Data;
using LaptopStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using LaptopStore.ViewModels;
namespace LaptopStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString("Cart");

            if (cart == null)
                return View(new List<CartItem>());

            var cartItems = JsonSerializer.Deserialize<List<CartItem>>(cart);

            return View(cartItems);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            var cart = HttpContext.Session.GetString("Cart");

            List<CartItem> cartItems = cart == null
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cart);

            var existing = cartItems.FirstOrDefault(x => x.Product.Id == id);

            if (existing != null)
                existing.Quantity++;
            else
                cartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });

            HttpContext.Session.SetString(
                "Cart",
                JsonSerializer.Serialize(cartItems));

            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetString("Cart");

            if (cart != null)
            {
                var cartItems = System.Text.Json.JsonSerializer
                    .Deserialize<List<CartItem>>(cart);

                var item = cartItems.FirstOrDefault(x => x.Product.Id == id);

                if (item != null)
                {
                    cartItems.Remove(item);
                }

                HttpContext.Session.SetString(
                    "Cart",
                    System.Text.Json.JsonSerializer.Serialize(cartItems));
            }

            return RedirectToAction("Index");
        }
        public IActionResult Increase(int id)
        {
            var cart = HttpContext.Session.GetString("Cart");

            if (cart != null)
            {
                var cartItems = System.Text.Json.JsonSerializer
                    .Deserialize<List<CartItem>>(cart);

                var item = cartItems.FirstOrDefault(x => x.Product.Id == id);

                if (item != null)
                {
                    item.Quantity++;
                }

                HttpContext.Session.SetString(
                    "Cart",
                    System.Text.Json.JsonSerializer.Serialize(cartItems));
            }

            return RedirectToAction("Index");
        }
        public IActionResult Decrease(int id)
        {
            var cart = HttpContext.Session.GetString("Cart");

            if (cart != null)
            {
                var cartItems = System.Text.Json.JsonSerializer
                    .Deserialize<List<CartItem>>(cart);

                var item = cartItems.FirstOrDefault(x => x.Product.Id == id);

                if (item != null)
                {
                    item.Quantity--;

                    if (item.Quantity <= 0)
                    {
                        cartItems.Remove(item);
                    }
                }

                HttpContext.Session.SetString(
                    "Cart",
                    System.Text.Json.JsonSerializer.Serialize(cartItems));
            }

            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            var cart = HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cart))
                return RedirectToAction("Index");

            var cartItems = System.Text.Json.JsonSerializer
                .Deserialize<List<CartItem>>(cart);

            var customer = new Customer
            {
                FullName = model.FullName,
                Phone = model.Phone,
                Email = model.Email,
                Address = model.Address
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            var order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now,
                Status = "Pending",
                Note = model.Note,
                TotalAmount = cartItems.Sum(x =>
                    x.Product.Price * x.Quantity)
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }

            _context.SaveChanges();

            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
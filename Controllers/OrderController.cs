using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ElectroShop.Models;
using ElectroShop.Models.ViewModels;
using ElectroShop.Data;
using ElectroShop.Services;
using System.Text.Json;
using System.Security.Claims;

namespace ElectroShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductService _productService;
        private const string CartSessionKey = "Cart";

        public OrderController(ApplicationDbContext context, ProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
            
            return View(orders);
        }

        public IActionResult Checkout()
        {
            var cartItems = GetCartItems();
            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var model = new CheckoutViewModel
            {
                CartItems = cartItems,
                Total = cartItems.Sum(x => x.Total),
                ShippingInfo = new ShippingInfo()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ProcessOrder(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CartItems = GetCartItems();
                model.Total = model.CartItems.Sum(x => x.Total);
                return View("Checkout", model);
            }

            var cartItems = GetCartItems();
            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Items = cartItems,
                Total = cartItems.Sum(x => x.Total),
                Status = "pending",
                PaymentMethod = model.PaymentMethod,
                ShippingInfo = model.ShippingInfo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // Clear cart
            HttpContext.Session.Remove(CartSessionKey);

            return RedirectToAction("OrderSuccess", new { orderId = order.Id });
        }

        public IActionResult OrderSuccess(string orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Details(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = _context.Orders.FirstOrDefault(o => o.Id == id && o.UserId == userId);
            
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public IActionResult CancelOrder(string orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId && o.UserId == userId);
            
            if (order == null || order.Status != "pending")
            {
                return Json(new { success = false, message = "Không thể hủy đơn hàng này" });
            }

            order.Status = "cancelled";
            order.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            return Json(new { success = true, message = "Đã hủy đơn hàng thành công" });
        }

        private List<CartItem> GetCartItems()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }

            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }
    }

}
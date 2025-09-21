using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ElectroShop.Data;
using ElectroShop.Models;
using ElectroShop.Models.ViewModels;

namespace ElectroShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stats = new AdminDashboardViewModel
            {
                TotalProducts = await _context.Products.CountAsync(),
                TotalUsers = await _context.Users.Where(u => u.Role == "customer").CountAsync(),
                TotalOrders = await _context.Orders.CountAsync(),
                TotalRevenue = await _context.Orders.Where(o => o.Status == "delivered").SumAsync(o => o.Total),
                RecentOrders = await _context.Orders.OrderByDescending(o => o.CreatedAt).Take(5).ToListAsync(),
                LowStockProducts = await _context.Products.Where(p => !p.InStock).Take(5).ToListAsync()
            };

            return View(stats);
        }

        // Products Management
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid().ToString();
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sản phẩm đã được tạo thành công!";
                return RedirectToAction("Products");
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sản phẩm đã được cập nhật thành công!";
                return RedirectToAction("Products");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sản phẩm đã được xóa thành công!";
            }
            return RedirectToAction("Products");
        }

        // Orders Management
        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders.OrderByDescending(o => o.CreatedAt).ToListAsync();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(string id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = status;
                order.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Trạng thái đơn hàng đã được cập nhật!";
            }
            return RedirectToAction("Orders");
        }

        // Users Management
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.Where(u => u.Role == "customer").ToListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleUserStatus(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Trạng thái người dùng đã được {(user.IsActive ? "kích hoạt" : "vô hiệu hóa")}!";
            }
            return RedirectToAction("Users");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using ElectroShop.Models;
using ElectroShop.Services;
using System.Text.Json;

namespace ElectroShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductService _productService;
        private const string CartSessionKey = "Cart";

        public CartController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var cartItems = GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(string productId, int quantity = 1)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cartItems = GetCartItems();
            var existingItem = cartItems.FirstOrDefault(x => x.Product.Id == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem { Product = product, Quantity = quantity });
            }

            SaveCartItems(cartItems);
            return Json(new { success = true, totalItems = cartItems.Sum(x => x.Quantity) });
        }

        [HttpPost]
        public IActionResult UpdateQuantity(string productId, int quantity)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(x => x.Product.Id == productId);

            if (item != null)
            {
                if (quantity <= 0)
                {
                    cartItems.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                SaveCartItems(cartItems);
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string productId)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(x => x.Product.Id == productId);

            if (item != null)
            {
                cartItems.Remove(item);
                SaveCartItems(cartItems);
            }

            return Json(new { success = true });
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

        private void SaveCartItems(List<CartItem> cartItems)
        {
            var cartJson = JsonSerializer.Serialize(cartItems);
            HttpContext.Session.SetString(CartSessionKey, cartJson);
        }
    }
}
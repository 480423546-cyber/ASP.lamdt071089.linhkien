using Microsoft.AspNetCore.Mvc;
using ElectroShop.Models.ViewModels;
using ElectroShop.Services;

namespace ElectroShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(string? search, string? category, string? brand, string? sort)
        {
            var viewModel = new ProductsViewModel
            {
                Products = _productService.SearchProducts(search, category, brand, sort),
                Categories = ProductService.Categories,
                Brands = ProductService.Brands,
                SelectedCategory = category,
                SelectedBrand = brand,
                SearchQuery = search,
                SortBy = sort
            };

            return View(viewModel);
        }

        public IActionResult Details(string id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
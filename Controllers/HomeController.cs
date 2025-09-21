using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ElectroShop.Models;
using ElectroShop.Models.ViewModels;
using ElectroShop.Services;

namespace ElectroShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProductService _productService;

    public HomeController(ILogger<HomeController> logger, ProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public IActionResult Index()
    {
        var viewModel = new HomeViewModel
        {
            FeaturedProducts = _productService.GetFeaturedProducts(),
            NewProducts = _productService.GetNewProducts()
        };
        return View(viewModel);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

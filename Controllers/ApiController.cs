using Microsoft.AspNetCore.Mvc;
using ElectroShop.Services;

[ApiController]
[Route("api/[controller]")]
public class ProductsApiController : ControllerBase
{
    private readonly ProductService _productService;
    
    public ProductsApiController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _productService.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(string id)
    {
        var product = _productService.GetProductById(id);
        return product != null ? Ok(product) : NotFound();
    }
}
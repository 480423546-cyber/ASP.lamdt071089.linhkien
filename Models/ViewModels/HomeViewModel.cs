namespace ElectroShop.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> FeaturedProducts { get; set; } = new();
        public List<Product> NewProducts { get; set; } = new();
    }
}
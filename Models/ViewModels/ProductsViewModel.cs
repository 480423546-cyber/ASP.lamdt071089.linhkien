namespace ElectroShop.Models.ViewModels
{
    public class ProductsViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public List<string> Brands { get; set; } = new();
        public string? SelectedCategory { get; set; }
        public string? SelectedBrand { get; set; }
        public string? SearchQuery { get; set; }
        public string? SortBy { get; set; }
    }
}
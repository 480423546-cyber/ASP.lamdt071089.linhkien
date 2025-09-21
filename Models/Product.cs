using System.ComponentModel.DataAnnotations;

namespace ElectroShop.Models
{
    public class Product
    {
        public string Id { get; set; } = string.Empty;
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Code { get; set; } = string.Empty;
        
        [Required]
        public decimal Price { get; set; }
        
        public decimal? OriginalPrice { get; set; }
        
        [Required]
        public string Image { get; set; } = string.Empty;
        
        [Required]
        public string Category { get; set; } = string.Empty;
        
        [Required]
        public string Brand { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        public Dictionary<string, string> Specifications { get; set; } = new();
        
        public bool InStock { get; set; } = true;
        
        public bool Featured { get; set; } = false;
        
        public double Rating { get; set; }
        
        public int Reviews { get; set; }
    }
}
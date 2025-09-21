namespace ElectroShop.Models
{
    public class CartItem
    {
        public Product Product { get; set; } = new();
        public int Quantity { get; set; } = 1;
        
        public decimal Total => Product.Price * Quantity;
    }
}
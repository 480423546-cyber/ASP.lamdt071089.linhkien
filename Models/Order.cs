using System.ComponentModel.DataAnnotations;

namespace ElectroShop.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        public List<CartItem> Items { get; set; } = new();
        
        [Required]
        public decimal Total { get; set; }
        
        [Required]
        public string Status { get; set; } = "pending"; // pending, confirmed, shipping, delivered, cancelled
        
        [Required]
        public string PaymentMethod { get; set; } = "cod"; // cod, bank_transfer, e_wallet
        
        public ShippingInfo ShippingInfo { get; set; } = new();
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class ShippingInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
    }
}
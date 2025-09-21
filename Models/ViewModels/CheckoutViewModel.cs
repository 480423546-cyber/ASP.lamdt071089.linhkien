using ElectroShop.Models;
using System.ComponentModel.DataAnnotations;

namespace ElectroShop.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; } = new();
        public decimal Total { get; set; }
        
        [Required]
        public ShippingInfo ShippingInfo { get; set; } = new();
        
        [Required]
        public string PaymentMethod { get; set; } = "cod";
    }
}
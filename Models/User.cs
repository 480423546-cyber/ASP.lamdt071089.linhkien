using System.ComponentModel.DataAnnotations;

namespace ElectroShop.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
        
        public string? Phone { get; set; }
        
        public string? Address { get; set; }
        
        [Required]
        public string Role { get; set; } = "customer"; // customer, admin
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; } = true;
    }
}
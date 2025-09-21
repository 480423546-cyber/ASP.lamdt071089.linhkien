using ElectroShop.Models;

namespace ElectroShop.Services
{
    public class ProductService
    {
        private static readonly List<Product> _products = new()
        {
            new Product
            {
                Id = "1",
                Name = "Arduino Uno R3",
                Code = "ARD-UNO-R3",
                Price = 450000,
                OriginalPrice = 500000,
                Image = "https://images.pexels.com/photos/2881224/pexels-photo-2881224.jpeg?auto=compress&cs=tinysrgb&w=400",
                Category = "Board Mạch",
                Brand = "Arduino",
                Description = "Board mạch Arduino Uno R3 chính hãng, lý tưởng cho các dự án DIY và học tập lập trình embedded.",
                Specifications = new Dictionary<string, string>
                {
                    {"Vi điều khiển", "ATmega328P"},
                    {"Điện áp hoạt động", "5V"},
                    {"Điện áp đầu vào", "7-12V"},
                    {"Digital I/O", "14 pins"},
                    {"Analog Input", "6 pins"},
                    {"Flash Memory", "32KB"}
                },
                InStock = true,
                Featured = true,
                Rating = 4.8,
                Reviews = 124
            },
            new Product
            {
                Id = "2",
                Name = "Raspberry Pi 4 Model B",
                Code = "RPI-4B-4GB",
                Price = 1850000,
                Image = "https://images.pexels.com/photos/2582937/pexels-photo-2582937.jpeg?auto=compress&cs=tinysrgb&w=400",
                Category = "Board Mạch",
                Brand = "Raspberry Pi",
                Description = "Máy tính mini Raspberry Pi 4 với RAM 4GB, hiệu suất mạnh mẽ cho các ứng dụng IoT và AI.",
                Specifications = new Dictionary<string, string>
                {
                    {"Processor", "Quad-core Cortex-A72 (ARM v8) 64-bit SoC @ 1.5GHz"},
                    {"RAM", "4GB LPDDR4-3200 SDRAM"},
                    {"Connectivity", "Dual-band 802.11ac, Bluetooth 5.0"},
                    {"USB", "2 × USB 3.0, 2 × USB 2.0"},
                    {"HDMI", "2 × micro-HDMI"},
                    {"GPIO", "40-pin"}
                },
                InStock = true,
                Featured = true,
                Rating = 4.9,
                Reviews = 89
            },
            new Product
            {
                Id = "3",
                Name = "Cảm Biến DHT22",
                Code = "DHT22-TH",
                Price = 65000,
                Image = "https://images.pexels.com/photos/163100/circuit-circuit-board-resistor-computer-163100.jpeg?auto=compress&cs=tinysrgb&w=400",
                Category = "Cảm Biến",
                Brand = "Generic",
                Description = "Cảm biến nhiệt độ và độ ẩm chính xác cao DHT22, phù hợp cho các dự án giám sát môi trường.",
                Specifications = new Dictionary<string, string>
                {
                    {"Điện áp hoạt động", "3.3-6V"},
                    {"Dải đo nhiệt độ", "-40°C đến +80°C"},
                    {"Độ chính xác nhiệt độ", "±0.5°C"},
                    {"Dải đo độ ẩm", "0-100%RH"},
                    {"Độ chính xác độ ẩm", "±2%RH"},
                    {"Giao thức", "1-Wire"}
                },
                InStock = true,
                Rating = 4.7,
                Reviews = 156
            },
            new Product
            {
                Id = "9",
                Name = "Module ESP32",
                Code = "ESP32-WROOM",
                Price = 185000,
                Image = "https://images.pexels.com/photos/2881224/pexels-photo-2881224.jpeg?auto=compress&cs=tinysrgb&w=400",
                Category = "Module Wifi",
                Brand = "Espressif",
                Description = "Module ESP32 với WiFi và Bluetooth tích hợp, mạnh mẽ cho IoT và các ứng dụng không dây.",
                Specifications = new Dictionary<string, string>
                {
                    {"Processor", "Dual-core Tensilica LX6"},
                    {"Clock Speed", "240MHz"},
                    {"Flash", "4MB"},
                    {"SRAM", "520KB"},
                    {"WiFi", "802.11 b/g/n"},
                    {"Bluetooth", "v4.2 BR/EDR và BLE"}
                },
                InStock = true,
                Featured = true,
                Rating = 4.8,
                Reviews = 178
            },
            new Product
            {
                Id = "16",
                Name = "Nguồn 12V 2A",
                Code = "PSU-12V-2A",
                Price = 280000,
                OriginalPrice = 320000,
                Image = "https://images.pexels.com/photos/276489/pexels-photo-276489.jpeg?auto=compress&cs=tinysrgb&w=400",
                Category = "Nguồn",
                Brand = "Mean Well",
                Description = "Adapter nguồn 12V 2A chất lượng cao, ổn định và bảo vệ quá tải cho các dự án điện tử.",
                Specifications = new Dictionary<string, string>
                {
                    {"Điện áp đầu vào", "100-240V AC"},
                    {"Điện áp đầu ra", "12V DC"},
                    {"Dòng đầu ra", "2A"},
                    {"Công suất", "24W"},
                    {"Hiệu suất", ">85%"},
                    {"Bảo vệ", "OVP, OCP, SCP"}
                },
                InStock = true,
                Featured = true,
                Rating = 4.9,
                Reviews = 201
            }
        };

        public static readonly List<string> Categories = new()
        {
            "Tất cả", "Board Mạch", "Cảm Biến", "IC", "Transistor", "Điện Trở",
            "Tụ Điện", "LED", "Module Wifi", "Motor", "Display", "Phụ Kiện",
            "Relay", "Biến Trở", "Nguồn"
        };

        public static readonly List<string> Brands = new()
        {
            "Tất cả", "Arduino", "Raspberry Pi", "Generic", "Texas Instruments",
            "Fairchild", "Nichicon", "Espressif", "TowerPro", "ALPS", "Mean Well"
        };

        public List<Product> GetAllProducts() => _products;

        public List<Product> GetFeaturedProducts() => _products.Where(p => p.Featured).ToList();

        public List<Product> GetNewProducts() => _products.Take(4).ToList();

        public Product? GetProductById(string id) => _products.FirstOrDefault(p => p.Id == id);

        public List<Product> SearchProducts(string? query = null, string? category = null, string? brand = null, string? sortBy = null)
        {
            var products = _products.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                products = products.Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                             p.Description.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(category) && category != "Tất cả")
            {
                products = products.Where(p => p.Category == category);
            }

            if (!string.IsNullOrEmpty(brand) && brand != "Tất cả")
            {
                products = products.Where(p => p.Brand == brand);
            }

            return sortBy switch
            {
                "price_asc" => products.OrderBy(p => p.Price).ToList(),
                "price_desc" => products.OrderByDescending(p => p.Price).ToList(),
                "name" => products.OrderBy(p => p.Name).ToList(),
                "rating" => products.OrderByDescending(p => p.Rating).ToList(),
                _ => products.ToList()
            };
        }
    }
}
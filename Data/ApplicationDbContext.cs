using Microsoft.EntityFrameworkCore;
using ElectroShop.Models;
using System.Text.Json;

namespace ElectroShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.OriginalPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Specifications)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions)null) ?? new Dictionary<string, string>()
                    );
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
            });

            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("datetime('now')");
                entity.Property(e => e.Items)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<List<CartItem>>(v, (JsonSerializerOptions)null) ?? new List<CartItem>()
                    );
                entity.Property(e => e.ShippingInfo)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<ShippingInfo>(v, (JsonSerializerOptions)null) ?? new ShippingInfo()
                    );
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "admin-001",
                    Email = "admin@electroshop.vn",
                    Name = "Quản trị viên",
                    Phone = "0123456789",
                    Address = "123 Đường ABC, TP.HCM",
                    Role = "admin",
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123") // Mật khẩu: admin123
                }
            );

            // Seed sample users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "user-001",
                    Email = "nguyen.van.a@gmail.com",
                    Name = "Nguyễn Văn A",
                    Phone = "0901234567",
                    Address = "123 Đường Lê Lợi, Q1, TP.HCM",
                    Role = "customer",
                    CreatedAt = DateTime.Now.AddDays(-30),
                    IsActive = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("123456")
                },
                new User
                {
                    Id = "user-002",
                    Email = "tran.thi.b@gmail.com",
                    Name = "Trần Thị B",
                    Phone = "0912345678",
                    Address = "456 Đường Nguyễn Huệ, Q1, TP.HCM",
                    Role = "customer",
                    CreatedAt = DateTime.Now.AddDays(-25),
                    IsActive = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("123456")
                },
                new User
                {
                    Id = "user-003",
                    Email = "le.van.c@gmail.com",
                    Name = "Lê Văn C",
                    Phone = "0923456789",
                    Address = "789 Đường Hai Bà Trưng, Q3, TP.HCM",
                    Role = "customer",
                    CreatedAt = DateTime.Now.AddDays(-20),
                    IsActive = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("123456")
                },
                new User
                {
                    Id = "user-004",
                    Email = "pham.thi.d@gmail.com",
                    Name = "Phạm Thị D",
                    Phone = "0934567890",
                    Role = "customer",
                    CreatedAt = DateTime.Now.AddDays(-15),
                    IsActive = false,
                    Password = BCrypt.Net.BCrypt.HashPassword("123456")
                }
            );

            // Seed sample products
            var products = new[]
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
                    Id = "4",
                    Name = "Module ESP32",
                    Code = "ESP32-WROOM",
                    Price = 185000,
                    Image = "https://images.pexels.com/photos/442154/pexels-photo-442154.jpeg?auto=compress&cs=tinysrgb&w=400",
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
                    Id = "5",
                    Name = "Servo Motor SG90",
                    Code = "SERVO-SG90",
                    Price = 95000,
                    Image = "https://images.pexels.com/photos/159298/gears-cogs-machine-machinery-159298.jpeg?auto=compress&cs=tinysrgb&w=400",
                    Category = "Motor",
                    Brand = "TowerPro",
                    Description = "Servo motor SG90 nhỏ gọn, xoay 180 độ, thích hợp cho robot và các dự án automation.",
                    Specifications = new Dictionary<string, string>
                    {
                        {"Góc xoay", "180°"},
                        {"Điện áp", "4.8-6V"},
                        {"Tốc độ", "0.1s/60°"},
                        {"Moment xoắn", "1.8kg.cm"},
                        {"Kích thước motor", "22.2 x 11.8 x 31mm"},
                        {"Trọng lượng", "9g"}
                    },
                    InStock = false,
                    Rating = 4.6,
                    Reviews = 134
                },
                new Product
                {
                    Id = "6",
                    Name = "Màn Hình LCD 16x2",
                    Code = "LCD-16X2-I2C",
                    Price = 125000,
                    Image = "https://images.pexels.com/photos/442150/pexels-photo-442150.jpeg?auto=compress&cs=tinysrgb&w=400",
                    Category = "Display",
                    Brand = "Generic",
                    Description = "Màn hình LCD 16x2 với module I2C, dễ dàng hiển thị thông tin trong các dự án.",
                    Specifications = new Dictionary<string, string>
                    {
                        {"Kích thước", "16x2 ký tự"},
                        {"Giao tiếp", "I2C (2 dây)"},
                        {"Điện áp", "5V"},
                        {"Màu nền", "Xanh lá"},
                        {"Kích thước LCD", "80 x 36 x 12mm"},
                        {"Ký tự", "5x8 dots"}
                    },
                    InStock = true,
                    Rating = 4.4,
                    Reviews = 98
                },
                new Product
                {
                    Id = "7",
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
                },
                new Product
                {
                    Id = "8",
                    Name = "Breadboard 830 điểm",
                    Code = "BB-830-WHITE",
                    Price = 85000,
                    Image = "https://images.pexels.com/photos/343457/pexels-photo-343457.jpeg?auto=compress&cs=tinysrgb&w=400",
                    Category = "Phụ Kiện",
                    Brand = "Generic",
                    Description = "Breadboard 830 điểm chất lượng cao, lý tưởng để tạo mẫu và thử nghiệm mạch điện.",
                    Specifications = new Dictionary<string, string>
                    {
                        {"Số điểm", "830 tie-points"},
                        {"Kích thước board", "165 x 55 x 10mm"},
                        {"Pitch", "2.54mm"},
                        {"Màu sắc", "Trắng"},
                        {"Vật liệu", "ABS plastic"},
                        {"Điện áp tối đa", "15V"}
                    },
                    InStock = false,
                    Rating = 4.3,
                    Reviews = 76
                }
            };

            // Seed sample orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = "order-001",
                    UserId = "user-001",
                    Items = new List<CartItem>(),
                    Total = 635000,
                    Status = "delivered",
                    PaymentMethod = "cod",
                    ShippingInfo = new ShippingInfo
                    {
                        Name = "Nguyễn Văn A",
                        Phone = "0901234567",
                        Address = "123 Đường Lê Lợi",
                        City = "TP.HCM",
                        District = "Quận 1"
                    },
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-2)
                },
                new Order
                {
                    Id = "order-002",
                    UserId = "user-002",
                    Items = new List<CartItem>(),
                    Total = 1850000,
                    Status = "shipping",
                    PaymentMethod = "bank_transfer",
                    ShippingInfo = new ShippingInfo
                    {
                        Name = "Trần Thị B",
                        Phone = "0912345678",
                        Address = "456 Đường Nguyễn Huệ",
                        City = "TP.HCM",
                        District = "Quận 1"
                    },
                    CreatedAt = DateTime.Now.AddDays(-3),
                    UpdatedAt = DateTime.Now.AddDays(-1)
                },
                new Order
                {
                    Id = "order-003",
                    UserId = "user-003",
                    Items = new List<CartItem>(),
                    Total = 250000,
                    Status = "confirmed",
                    PaymentMethod = "cod",
                    ShippingInfo = new ShippingInfo
                    {
                        Name = "Lê Văn C",
                        Phone = "0923456789",
                        Address = "789 Đường Hai Bà Trưng",
                        City = "TP.HCM",
                        District = "Quận 3"
                    },
                    CreatedAt = DateTime.Now.AddDays(-2),
                    UpdatedAt = DateTime.Now.AddDays(-1)
                },
                new Order
                {
                    Id = "order-004",
                    UserId = "user-001",
                    Items = new List<CartItem>(),
                    Total = 185000,
                    Status = "pending",
                    PaymentMethod = "e_wallet",
                    ShippingInfo = new ShippingInfo
                    {
                        Name = "Nguyễn Văn A",
                        Phone = "0901234567",
                        Address = "123 Đường Lê Lợi",
                        City = "TP.HCM",
                        District = "Quận 1"
                    },
                    CreatedAt = DateTime.Now.AddHours(-6),
                    UpdatedAt = DateTime.Now.AddHours(-6)
                },
                new Order
                {
                    Id = "order-005",
                    UserId = "user-002",
                    Items = new List<CartItem>(),
                    Total = 95000,
                    Status = "cancelled",
                    PaymentMethod = "cod",
                    ShippingInfo = new ShippingInfo
                    {
                        Name = "Trần Thị B",
                        Phone = "0912345678",
                        Address = "456 Đường Nguyễn Huệ",
                        City = "TP.HCM",
                        District = "Quận 1"
                    },
                    CreatedAt = DateTime.Now.AddDays(-7),
                    UpdatedAt = DateTime.Now.AddDays(-6)
                }
            );

            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
# ElectroShop - Website Bán Linh Kiện Điện Tử

## Tổng quan
Website bán linh kiện điện tử được chuyển đổi từ React sang ASP.NET Core MVC với đầy đủ tính năng:

### Tính năng chính:
- ✅ **Cơ sở dữ liệu SQLite** - Lưu trữ sản phẩm, người dùng, đơn hàng
- ✅ **Đăng ký/Đăng nhập** - Xác thực người dùng với mã hóa mật khẩu BCrypt
- ✅ **Trang quản trị Admin** - Quản lý sản phẩm, đơn hàng, khách hàng
- ✅ **Giỏ hàng** - Thêm/xóa/cập nhật sản phẩm
- ✅ **Responsive Design** - Giao diện đẹp với Tailwind CSS
- ✅ **Tìm kiếm & Lọc** - Tìm kiếm sản phẩm theo danh mục, thương hiệu

## Tài khoản quản trị viên
```
Email: admin@electroshop.vn
Mật khẩu: admin123
```

## Cách chạy dự án

### 1. Cài đặt dependencies
```bash
cd "D:\New folder\ASP.NET\07092025-nguyennhutlam\website bán linh kiện điện tử\linhkiendientu3\ElectroShop"
dotnet restore
```

### 2. Build dự án
```bash
dotnet build
```

### 3. Chạy dự án
```bash
dotnet run --urls "http://localhost:8080"
```

### 4. Truy cập website
- **Trang chủ**: http://localhost:8080
- **Trang admin**: http://localhost:8080/Admin (cần đăng nhập admin)
- **Đăng nhập**: http://localhost:8080/Account/Login

## Cấu trúc dự án

```
ElectroShop/
├── Controllers/           # Controllers xử lý request
│   ├── HomeController.cs     # Trang chủ, About, Contact
│   ├── ProductsController.cs # Danh sách & chi tiết sản phẩm
│   ├── CartController.cs     # Giỏ hàng
│   ├── AccountController.cs  # Đăng nhập/đăng ký
│   └── AdminController.cs    # Quản trị admin
├── Models/               # Models & ViewModels
│   ├── Product.cs           # Model sản phẩm
│   ├── User.cs             # Model người dùng
│   ├── Order.cs            # Model đơn hàng
│   └── ViewModels/         # ViewModels cho Views
├── Views/                # Razor Views
│   ├── Home/              # Trang chủ, About, Contact
│   ├── Products/          # Danh sách & chi tiết sản phẩm
│   ├── Cart/              # Giỏ hàng
│   ├── Account/           # Đăng nhập/đăng ký
│   ├── Admin/             # Trang quản trị
│   └── Shared/            # Layout chung
├── Data/                 # Database Context
│   └── ApplicationDbContext.cs
├── Services/             # Business Logic
│   ├── ProductService.cs    # Xử lý sản phẩm
│   └── AuthService.cs      # Xác thực người dùng
└── wwwroot/              # Static files (CSS, JS, Images)
```

## Tính năng Admin

### Dashboard
- Thống kê tổng quan: sản phẩm, khách hàng, đơn hàng, doanh thu
- Đơn hàng gần đây
- Sản phẩm hết hàng

### Quản lý sản phẩm
- Xem danh sách sản phẩm
- Thêm sản phẩm mới
- Sửa thông tin sản phẩm
- Xóa sản phẩm

### Quản lý đơn hàng
- Xem danh sách đơn hàng
- Chi tiết đơn hàng
- Cập nhật trạng thái đơn hàng

### Quản lý khách hàng
- Xem danh sách khách hàng
- Kích hoạt/vô hiệu hóa tài khoản

## Cơ sở dữ liệu

### Tables:
- **Users**: Thông tin người dùng (khách hàng & admin)
- **Products**: Thông tin sản phẩm
- **Orders**: Đơn hàng (sẽ được mở rộng trong tương lai)

### Seed Data:
- 1 tài khoản admin mặc định
- 2 sản phẩm mẫu (Arduino Uno R3, Raspberry Pi 4)

## Công nghệ sử dụng

- **Backend**: ASP.NET Core 9.0 MVC
- **Database**: SQLite với Entity Framework Core
- **Authentication**: Cookie Authentication
- **Password Hashing**: BCrypt.Net
- **Frontend**: Tailwind CSS
- **JavaScript**: jQuery cho AJAX calls

## Lưu ý

1. **Database**: File SQLite sẽ được tạo tự động khi chạy lần đầu
2. **Session**: Giỏ hàng sử dụng Session để lưu trữ
3. **Images**: Sử dụng URL từ Pexels cho hình ảnh sản phẩm
4. **Responsive**: Giao diện tương thích mobile và desktop

## Troubleshooting

### Lỗi build do process đang chạy:
```bash
# Dừng tất cả process dotnet
taskkill /f /im dotnet.exe
# Hoặc restart máy tính
```

### Lỗi database:
```bash
# Xóa database và tạo lại
rm electroshop.db
dotnet run
```

### Port đã được sử dụng:
```bash
# Thay đổi port khác
dotnet run --urls "http://localhost:8081"
```

## Tính năng sẽ phát triển

- [ ] Thanh toán online
- [ ] Quản lý inventory
- [ ] Email notifications
- [ ] Reviews & ratings
- [ ] Wishlist
- [ ] Advanced search
- [ ] Reports & analytics

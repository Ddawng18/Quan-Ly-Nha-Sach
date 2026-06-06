# Quan-Ly-Nha-Sach (BookStore Management System)

Ứng dụng quản lý nhà sách desktop được xây dựng trên **.NET 9 Windows Forms** với kiến trúc phân lớp rõ ràng.

---

## Mục lục

- [Tổng quan](#tổng-quan)
- [Tính năng chính](#tính-năng-chính)
- [Công nghệ](#công-nghệ)
- [Cấu trúc project](#cấu-trúc-project)
- [Kiến trúc](#kiến-trúc)
- [Cài đặt & Chạy](#cài-đặt--chạy)
- [Cấu hình database](#cấu-hình-database)
- [Thanh toán QR](#thanh-toán-qr)
- [Tài khoản đăng nhập](#tài-khoản-đăng-nhập)
- [Ghi chú phát triển](#ghi-chú-phát-triển)

---

## Tổng quan

BookStoreApp là ứng dụng desktop quản lý nhà sách với đầy đủ tính năng: quản lý sách, khách hàng, nhân viên, nhà cung cấp, danh mục, hóa đơn báo cáo và bán hàng POS (Point of Sale) tích hợp thanh toán QR.

Ứng dụng sử dụng **SQL Server** làm database thông qua **ADO.NET** (`Microsoft.Data.SqlClient`) và kiến trúc phân lớp **DTO → DAL → BLL → UI**.

---

## Tính năng chính

### Quản lý Sách
- Thêm / Sửa / Xóa sách với đầy đủ thông tin: ISBN, tiêu đề, tác giả, nhà xuất bản, năm XB, giá nhập, giá bán, tồn kho
- Chọn **Danh mục (Category)** và **Nhà cung cấp (Supplier)** khi thêm/sửa sách
- Tìm kiếm theo tiêu đề, tác giả, ISBN
- Lọc theo danh mục, nhà xuất bản, mức tồn kho (Còn hàng / Sắp hết / Hết hàng)

### Quản lý Khách hàng
- CRUD khách hàng với lịch sử mua hàng
- Tích điểm thưởng (loyalty points) khi mua hàng
- Quy đổi điểm thành tiền giảm giá tại quầy POS

### Quản lý Nhân viên
- CRUD nhân viên với vai trò **Admin** (toàn quyền) và **Staff** (bán hàng + xem kho)
- Giao diện phân quyền: Staff sẽ ẩn các chức năng quản trị

### Bán hàng POS (Point of Sale) — `PosForm`
- Giỏ hàng: thêm / xóa sách, điều chỉnh số lượng
- Giảm giá từng dòng và giảm giá toàn hóa đơn (phần trăm hoặc số tiền cố định)
- Tính thuế, quy đổi điểm thưởng
- **Thanh toán Tiền mặt** — lưu hóa đơn ngay
- **Thanh toán QR** — mở form hiển thị mã QR, tự động kiểm tra trạng thái

### Quản lý Hóa đơn
- Danh sách hóa đơn với lọc theo ngày và trạng thái
- Xem chi tiết hóa đơn (sản phẩm, khách hàng, nhân viên, phương thức thanh toán)
- Cập nhật trạng thái: Pending → Paid → Cancelled

### Báo cáo & Thống kê
- Doanh thu theo ngày / tuần / tháng (biểu đồ OxyPlot)
- Top N sách bán chạy nhất
- Cảnh báo sách sắp hết hàng
- Báo cáo sách bán chậm (90 ngày)

---

## Công nghệ

| Thành phần | Công nghệ |
|-----------|-----------|
| **Runtime** | .NET 9 |
| **UI Framework** | Windows Forms |
| **Ngôn ngữ** | C# 13 |
| **Database** | SQL Server (ADO.NET) |
| **ADO.NET Provider** | Microsoft.Data.SqlClient 5.2.2 |
| **Biểu đồ** | OxyPlot.WindowsForms 2.2.0 |
| **Cấu hình** | Microsoft.Extensions.Configuration.Json |
| **Payment APIs** | MoMo v2 sandbox, VNPay sandbox |

---

## Cấu trúc project

```
BookStoreApp.sln
├── DTO/                          # Data Transfer Objects (Models, Enums)
│   ├── Books/                    # Book, BookViewDto, BookFilter, StockLevelFilter
│   ├── Customers/                # Customer, CustomerPurchaseDto
│   ├── Employees/                # Employee
│   ├── Orders/                   # Order, OrderViewDto, OrderDetail, OrderStatus
│   ├── POS/                      # CartLine, CartTotals, CheckoutRequest, CheckoutResult, DiscountType
│   ├── Payments/                 # PaymentConfig, PaymentStatus
│   ├── Reports/                  # ReportRowDto, ReportSectionDto
│   ├── Dashboard/                # DashboardMetricDto, BestSellingBookDto
│   ├── Category/, Supplier/      # Category & Supplier entities
│   └── BookStoreApp.DTO.csproj
│
├── DAL/                          # Data Access Layer
│   ├── Interfaces/               # IBookRepository, ICustomerRepository, IOrderRepository, ...
│   ├── Repositories/             # SQL Server implementations (ADO.NET)
│   ├── DbConnectionFactory.cs    # SqlConnection factory
│   └── BookStoreApp.DAL.csproj
│
├── BLL/                          # Business Logic Layer
│   ├── *Service.cs               # BookService, CustomerService, OrderService, PosService, ...
│   ├── I*Service.cs              # Interfaces
│   ├── Payments/                 # DemoPaymentProvider, MomoPaymentProvider, VNPayPaymentProvider
│   └── BookStoreApp.BLL.csproj
│
├── Utilities/                    # Tiện ích chung
│   ├── FileLogger.cs             # Ghi log file
│   ├── ReportExporter.cs         # Xuất báo cáo CSV
│   └── BookStoreApp.Utilities.csproj
│
├── BookStoreApp/                 # WinForms UI
│   ├── Forms/                    # LoginForm, MainForm, PosForm, OrderCreateForm, BookEditForm, PaymentQRForm, ...
│   ├── UserControls/             # DashboardControl, BookControl, OrdersControl, ReportsControl, ...
│   ├── Theme/                    # AppBranding, AppTheme
│   ├── ServiceLocator.cs         # Manual Dependency Injection
│   ├── Program.cs                # Entry point
│   ├── appsettings.json          # Connection string + Payment config
│   └── BookStoreApp.csproj
│
└── README.md                     # Tài liệu này
```

---

## Kiến trúc

```
┌─────────────────────────────────────────────────┐
│  WinForms UI (Forms + UserControls)              │
│  Lấy service từ ServiceLocator                   │
├─────────────────────────────────────────────────┤
│  BLL (Service interfaces + implementations)      │
│  Business rules, validation, orchestration       │
├─────────────────────────────────────────────────┤
│  DAL (Repository interfaces + SQL impls)         │
│  ADO.NET — SqlConnection, SqlCommand             │
├─────────────────────────────────────────────────┤
│  DTO (Models, enums, validation)                 │
├─────────────────────────────────────────────────┤
│  Utilities (FileLogger, ReportExporter)          │
└─────────────────────────────────────────────────┘
```

**Dependency Injection** được thực hiện thủ công qua `ServiceLocator` trong `BookStoreApp/ServiceLocator.cs`. Tất cả UI controls lấy service từ đây thay vì tự khởi tạo.

---

## Cài đặt & Chạy

### Yêu cầu

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Windows 10/11
- SQL Server (LocalDB / Express / Developer)

### Build

```bash
dotnet build BookStoreApp.sln
```

### Chạy

```bash
dotnet run --project BookStoreApp
```

Hoặc chạy file exe:

```bash
.\BookStoreApp\bin\Debug\net9.0-windows\BookStoreApp.exe
```

---

## Cấu hình database

Connection string được đặt trong `BookStoreApp/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Nếu dùng **SQL Server Express**:
```json
"Server=.\\SQLEXPRESS;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

Nếu dùng **LocalDB**:
```json
"Server=(localdb)\\MSSQLLocalDB;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

File `appsettings.json` được copy ra output directory mỗi khi build.

---

## Thanh toán QR

### Cấu hình Payment

`BookStoreApp/appsettings.json`:

```json
{
  "Payment": {
    "DefaultProvider": "Demo",
    "QrTimeoutSeconds": 300,
    "PollingIntervalSeconds": 5,
    "MoMo": {
      "PartnerCode": "",
      "AccessKey": "",
      "SecretKey": "",
      "BaseUrl": "https://test-payment.momo.vn/v2/gateway/api/"
    },
    "VNPay": {
      "TmnCode": "",
      "HashSecret": "",
      "BaseUrl": "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"
    }
  }
}
```

| Provider | File | Mô tả |
|----------|------|-------|
| **Demo** | `BLL/Payments/DemoPaymentProvider.cs` | Tạo QR giả lập, tự confirm sau 10 giây |
| **MoMo** | `BLL/Payments/MomoPaymentProvider.cs` | API MoMo v2 sandbox (HMAC-SHA256) |
| **VNPay** | `BLL/Payments/VNPayPaymentProvider.cs` | API VNPay sandbox (HMAC-SHA512) |

Khi để trống credentials, hệ thống tự động fallback về **Demo**.

---

## Tài khoản đăng nhập

| Username | Password | Vai trò |
|----------|----------|---------|
| `admin` | `1` | Admin (toàn quyền) |
| `E` | `2` | Staff (POS + xem kho) |

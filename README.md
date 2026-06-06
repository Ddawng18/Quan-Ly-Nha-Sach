# Quan-Ly-Nha-Sach (BookStore Management System)

Ứng dụng quản lý nhà sách desktop được xây dựng trên **.NET 9 Windows Forms** với kiến trúc phân lớp rõ ràng (DTO → DAL → BLL → UI).

---

## Mục lục

- [Yêu cầu hệ thống](#yêu-cầu-hệ-thống)
- [Cài đặt SQL Server](#cài-đặt-sql-server)
- [Tạo database](#tạo-database)
- [Clone & Chạy project](#clone--chạy-project)
- [Cấu hình connection string](#cấu-hình-connection-string)
- [Tài khoản đăng nhập](#tài-khoản-đăng-nhập)
- [Cấu trúc project](#cấu-trúc-project)
- [Tính năng chính](#tính-năng-chính)
- [Thanh toán QR](#thanh-toán-qr)
- [Ghi chú phát triển](#ghi-chú-phát-triển)
- [Xử lý lỗi thường gặp](#xử-lý-lỗi-thường-gặp)

---

## Yêu cầu hệ thống

| Thành phần | Phiên bản / Yêu cầu |
|-----------|---------------------|
| **Hệ điều hành** | Windows 10 / 11 |
| **.NET SDK** | [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) |
| **SQL Server** | SQL Server 2022 / 2019 / Express / LocalDB |
| **IDE (khuyến nghị)** | Visual Studio 2022 |

---

## Cài đặt SQL Server

### Bước 1: Tải SQL Server

Chọn 1 trong 3 phiên bản:

| Phiên bản | Link tải | Ghi chú |
|-----------|----------|---------|
| **SQL Server Express** | https://www.microsoft.com/en-us/download/details.aspx?id=104781 | Miễn phí, đủ dùng |
| **SQL Server Developer** | https://www.microsoft.com/en-us/sql-server/sql-server-downloads | Miễn phí, đầy đủ tính năng |
| **LocalDB** | Đi kèm Visual Studio | Nhẹ, không cần cài service |

### Bước 2: Kiểm tra SQL Server đang chạy

Mở **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**.

Nếu chưa có SSMS, tải tại: https://aka.ms/ssmsfullsetup

Kiểm tra tên instance server của bạn:
- Mở SSMS → Object Explorer → xem tên server (ví dụ: `localhost`, `.\SQLEXPRESS`, `(localdb)\MSSQLLocalDB`)

---

## Tạo database

### Cách 1: Chạy script SQL (khuyến nghị)

1. Mở SSMS → Connect đến server của bạn
2. File → Open → File → chọn `database/init-database.sql`
3. Nhấn **Execute** (F5)

Script sẽ:
- Tạo database `BookStoreDb`
- Tạo 8 bảng: Categories, Suppliers, Employees, Accounts, Customers, Books, Orders, OrderDetails
- Insert dữ liệu mẫu (categories, suppliers, books, employees, accounts, customers)

### Cách 2: Tạo thủ công

Nếu không dùng script, tạo database rỗng:
```sql
CREATE DATABASE BookStoreDb;
```

> **Lưu ý:** App sẽ lỗi nếu chưa có bảng. Các bảng cần thiết xem trong file `database/init-database.sql`.

---

## Clone & Chạy project

### Bước 1: Clone repo

```bash
git clone https://github.com/Ddawng18/Quan-Ly-Nha-Sach.git
cd Quan-Ly-Nha-Sach
```

### Bước 2: Build

```bash
dotnet build BookStoreApp.sln
```

Hoặc trong Visual Studio: `Ctrl + Shift + B`

### Bước 3: Chạy

```bash
dotnet run --project BookStoreApp
```

Hoặc trong Visual Studio: nhấn `F5`

Hoặc chạy file exe:
```bash
.\BookStoreApp\bin\Debug\net9.0-windows\BookStoreApp.exe
```

---

## Cấu hình connection string

Mở file `BookStoreApp/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Sửa `Server=` theo SQL Server của bạn:**

| Loại SQL Server | Connection String |
|-----------------|-------------------|
| SQL Server mặc định | `Server=localhost;...` hoặc `Server=.;...` |
| SQL Server Express | `Server=.\SQLEXPRESS;...` |
| LocalDB | `Server=(localdb)\MSSQLLocalDB;...` |

Ví dụ đầy đủ cho SQL Server Express:
```json
"Server=.\SQLEXPRESS;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

> **Lưu ý:** File `appsettings.json` được copy ra thư mục output mỗi khi build (`CopyToOutputDirectory`).

---

## Tài khoản đăng nhập

Sau khi tạo database bằng script, app có sẵn 2 tài khoản:

| Username | Password | Vai trò | Quyền hạn |
|----------|----------|---------|-----------|
| `admin` | `admin` | Admin | Toàn quyền (Sách, NCC, Danh mục, NV, KH, Hóa đơn, Báo cáo) |
| `staff` | `staff` | Staff | Bán hàng POS + Xem kho |

---

## Cấu trúc project

```
Quan-Ly-Nha-Sach/
├── database/
│   └── init-database.sql       -- Script tạo database + dữ liệu mẫu
│
├── DTO/                          # Data Transfer Objects
│   ├── Books/                    # Book, BookViewDto, BookFilter
│   ├── Customers/                # Customer, CustomerPurchaseDto
│   ├── Employees/                # Employee
│   ├── Orders/                   # Order, OrderViewDto, OrderDetail
│   ├── POS/                      # CartLine, CheckoutRequest, DiscountType
│   ├── Payments/                 # PaymentConfig
│   ├── Reports/                  # ReportSectionDto
│   ├── Category/, Supplier/      # Category & Supplier
│   └── BookStoreApp.DTO.csproj
│
├── DAL/                          # Data Access Layer (ADO.NET)
│   ├── Interfaces/               # IBookRepository, IOrderRepository, ...
│   ├── Repositories/             # SQL Server implementations
│   ├── DbConnectionFactory.cs    # SqlConnection factory
│   └── BookStoreApp.DAL.csproj
│
├── BLL/                          # Business Logic Layer
│   ├── *Service.cs / I*Service.cs
│   ├── Payments/                 # MoMo, VNPay, Demo providers
│   └── BookStoreApp.BLL.csproj
│
├── Utilities/                    # Tiện ích
│   ├── FileLogger.cs
│   ├── ReportExporter.cs
│   └── BookStoreApp.Utilities.csproj
│
├── BookStoreApp/                 # WinForms UI
│   ├── Forms/                    # LoginForm, MainForm, PosForm, BookEditForm, ...
│   ├── UserControls/             # DashboardControl, BookControl, OrdersControl, ...
│   ├── Theme/                    # AppBranding, AppTheme
│   ├── ServiceLocator.cs         # Manual DI
│   ├── Program.cs                # Entry point
│   ├── appsettings.json          # Connection string + Payment config
│   └── BookStoreApp.csproj
│
├── BookStoreApp.sln
└── README.md
```

---

## Tính năng chính

### Quản lý Sách
- Thêm / Sửa / Xóa sách: ISBN, tiêu đề, tác giả, nhà xuất bản, năm XB, giá nhập, giá bán, tồn kho
- Chọn **Danh mục (Category)** và **Nhà cung cấp (Supplier)** khi thêm/sửa
- Tìm kiếm, lọc theo danh mục / nhà xuất bản / mức tồn kho

### Bán hàng POS (`PosForm`)
- Giỏ hàng, giảm giá từng dòng / toàn hóa đơn
- Tính thuế, quy đổi điểm thưởng
- **Tiền mặt** — lưu hóa đơn ngay
- **QR Payment** — hiển thị mã QR, tự động kiểm tra thanh toán

### Quản lý Hóa đơn
- Danh sách + chi tiết hóa đơn
- Lọc theo ngày, trạng thái (Pending / Paid / Cancelled)
- Cập nhật trạng thai đơn hàng

### Báo cáo
- Doanh thu theo ngày / tuần / tháng (biểu đồ OxyPlot)
- Top sách bán chạy, cảnh báo hết hàng, sách bán chậm
- Xuất CSV / Excel / PDF

---

## Thanh toán QR

### Cấu hình trong `appsettings.json`

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

| Provider | Mô tả |
|----------|-------|
| **Demo** | Giả lập QR, tự động confirm sau 10 giây. Mặc định khi chưa điền key. |
| **MoMo** | Gọi API sandbox MoMo v2 (HMAC-SHA256). Cần PartnerCode, AccessKey, SecretKey. |
| **VNPay** | Gọi API sandbox VNPay (HMAC-SHA512). Cần TmnCode, HashSecret. |

---

## Ghi chú phát triển

- Tất cả Form và UserControl dùng `.Designer.cs` để định nghĩa UI
- `ServiceLocator` cung cấp dependency injection thủ công
- Validation trả về `ValidationResult`, không throw exception cho business rules
- Ghi log qua `Utilities.FileLogger` (file `logs/bookstore-{date}.log`)

---

## Xử lý lỗi thường gặp

### Lỗi `A network-related or instance-specific error occurred...`
- SQL Server chưa chạy → Mở Services.msc → tìm `SQL Server` → Start
- Sai tên Server → Kiểm tra trong SSMS và sửa `appsettings.json`

### Lỗi `Invalid object name 'Books'`
- Chưa tạo bảng → Chạy lại `database/init-database.sql`

### Lỗi `Login failed for user`
- Sửa connection string thành Windows Authentication:
  ```
  Server=...;Database=BookStoreDb;Trusted_Connection=True;
  ```

### Build lỗi `NETSDK1045`
- Chưa cài .NET 9 SDK → Tải và cài đặt từ https://dotnet.microsoft.com/download/dotnet/9.0

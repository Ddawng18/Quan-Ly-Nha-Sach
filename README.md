# Quan-Ly-Nha-Sach (BookStore Management System)

Ứng dụng quản lý nhà sách desktop trên **.NET 9 Windows Forms** với kiến trúc phân lớp DTO → DAL → BLL → UI.

---

## Yêu cầu

| Thành phần | Phiên bản |
|-----------|-----------|
| Hệ điều hành | Windows 10 / 11 |
| .NET SDK | [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0) |
| SQL Server | SQL Server 2022 / 2019 / Express / LocalDB |
| IDE (khuyến nghị) | Visual Studio 2022 |

---

## Cách chạy code

### 1. Clone repo

```bash
git clone https://github.com/Ddawng18/Quan-Ly-Nha-Sach.git
cd Quan-Ly-Nha-Sach
```

### 2. Build

```bash
dotnet build BookStoreApp.sln
```

Hoặc trong Visual Studio: `Ctrl + Shift + B`

### 3. Chạy

```bash
dotnet run --project BookStoreApp
```

Hoặc trong Visual Studio: nhấn `F5`

---

## Kết nối database

### Bước 1: Cài SQL Server

Chọn 1 phiên bản và cài đặt:

- **SQL Server Express** (miễn phí): https://www.microsoft.com/en-us/download/details.aspx?id=104781
- **SQL Server Developer** (miễn phí, đầy đủ tính năng): https://www.microsoft.com/en-us/sql-server/sql-server-downloads
- **LocalDB** (nhẹ, đi kèm Visual Studio): chọn trong installer của Visual Studio

### Bước 2: Tạo database và bảng

Mở **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**, connect vào server của bạn, mở **New Query**, tự tạo database và các bảng theo code trong thư mục `DAL/Repositories/`.

Các bảng cần có:
- `Categories` (CategoryID, CategoryName)
- `Suppliers` (SupplierID, SupplierName, Address, Email, Phone)
- `Employees` (EmployeeID, FullName, Phone, Salary, Position, Role, CreatedDate)
- `Accounts` (AccountID, EmployeeID, Username, Password, Role, FullName, IsActive)
- `Customers` (CustomerID, FullName, Phone, Address, LoyaltyPoints, CreatedDate)
- `Books` (BookID, CategoryID, SupplierID, Title, Author, ISBN, Publisher, PublishYear, ImportPrice, SellPrice, QuantityInStock, LastImportDate, LastSoldDate, IsDeleted)
- `Orders` (OrderID, CustomerID, EmployeeID, OrderDate, SubtotalAmount, DiscountAmount, TaxAmount, TotalAmount, PaymentStatus, PaymentMethod, PaymentTransactionId, LoyaltyPointsEarned)
- `OrderDetails` (OrderDetailID, OrderID, BookID, Quantity, UnitPrice, DiscountAmount, Subtotal)

> **Lưu ý:** Bạn cần tự insert dữ liệu mẫu cho bảng `Accounts` để có thể đăng nhập. App kiểm tra username/password từ bảng này.

Ví dụ tạo database:
```sql
CREATE DATABASE BookStoreDb;
USE BookStoreDb;
```

Sau đó tạo các bảng và insert dữ liệu theo cấu trúc các cột ở trên.

### Bước 3: Sửa connection string

Mở file `BookStoreApp/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Thay `Server=` theo SQL Server của bạn:**

| Loại SQL Server | Giá trị |
|-----------------|---------|
| SQL Server mặc định | `Server=localhost;` hoặc `Server=.;` |
| SQL Server Express | `Server=.\SQLEXPRESS;` |
| LocalDB | `Server=(localdb)\MSSQLLocalDB;` |

Ví dụ cho SQL Server Express:
```json
"Server=.\SQLEXPRESS;Database=QuanLyNhaSach;Trusted_Connection=True;TrustServerCertificate=True;"
```

### Bước 4: Kiểm tra kết nối

Build và chạy app. Nếu hiện form đăng nhập là thành công.

---

## Tài khoản đăng nhập

App đọc từ bảng `Accounts`. Bạn cần tự insert tài khoản trong SQL:

Script SQL đã tạo sẵn 2 tài khoản:

| Username | Password | Vai trò |
|----------|----------|---------|
| `admin` | `1` | Admin (toàn quyền) |
| `E` | `2` | Staff (POS + xem kho) |

Nếu muốn thêm tài khoản khác, chạy lệnh SQL:
```sql
USE QuanLyNhaSach;
INSERT INTO Accounts (Username, Password, Role, FullName, IsActive)
VALUES ('staff', 'staff', 'Staff', N'Nhân viên', 1);
```

---

## Xử lý lỗi thường gặp

| Lỗi | Nguyên nhân | Cách fix |
|-----|-------------|----------|
| `A network-related or instance-specific error occurred...` | SQL Server chưa chạy hoặc sai tên server | Kiểm tra Services.msc → Start SQL Server; sửa `Server=` trong `appsettings.json` |
| `Invalid object name 'Books'` | Chưa tạo bảng | Tạo database và bảng trong SSMS |
| `Login failed for user` | Sai authentication | Dùng `Trusted_Connection=True` trong connection string |
| `NETSDK1045` | Chưa cài .NET 9 SDK | Tải và cài từ https://dotnet.microsoft.com/download/dotnet/9.0 |

---

## Tính năng chính

- **Quản lý sách**: CRUD, tìm kiếm, lọc, chọn Category và Supplier
- **POS bán hàng**: `PosForm` — giỏ hàng, giảm giá, thuế, điểm thưởng, thanh toán tiền mặt / QR
- **Hóa đơn**: Danh sách, chi tiết, cập nhật trạng thái
- **Báo cáo**: Doanh thu, sách bán chạy, cảnh báo hết hàng, biểu đồ OxyPlot
- **Phân quyền**: Admin (toàn quyền) / Staff (bán hàng + xem kho)

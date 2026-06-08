# Sơ đồ ERD - BookStore Management System

## ERD Diagram (Mermaid)

```mermaid
erDiagram
    Categories ||--o{ Books : "chứa"
    Suppliers ||--o{ Books : "cung cấp"
    Books ||--o{ OrderDetails : "nằm trong"
    Orders ||--|{ OrderDetails : "có"
    Customers ||--o{ Orders : "đặt"
    Employees ||--o{ Orders : "xử lý"
    Employees ||--o| Accounts : "có"

    Categories {
        int CategoryID PK "ID danh mục"
        nvarchar CategoryName "Tên danh mục"
    }

    Suppliers {
        int SupplierID PK "ID nhà cung cấp"
        nvarchar SupplierName "Tên NCC"
        nvarchar Address "Địa chỉ"
        nvarchar Email "Email"
        nvarchar Phone "SĐT"
    }

    Employees {
        int EmployeeID PK "ID nhân viên"
        nvarchar FullName "Họ tên"
        nvarchar Phone "SĐT"
        decimal Salary "Lương"
        nvarchar Position "Chức vụ"
        nvarchar Role "Vai trò (Admin/Staff)"
        datetime CreatedDate "Ngày tạo"
    }

    Accounts {
        int AccountID PK "ID tài khoản"
        int EmployeeID FK "ID nhân viên"
        nvarchar Username "Tên đăng nhập"
        nvarchar Password "Mật khẩu"
        nvarchar Role "Vai trò"
        nvarchar FullName "Họ tên"
        bit IsActive "Đang hoạt động"
    }

    Customers {
        int CustomerID PK "ID khách hàng"
        nvarchar FullName "Họ tên"
        nvarchar Phone "SĐT"
        nvarchar Address "Địa chỉ"
        int LoyaltyPoints "Điểm thưởng"
        datetime CreatedDate "Ngày tạo"
    }

    Books {
        int BookID PK "ID sách"
        int CategoryID FK "ID danh mục"
        int SupplierID FK "ID nhà cung cấp"
        nvarchar Title "Tiêu đề"
        nvarchar Author "Tác giả"
        nvarchar ISBN "Mã ISBN"
        nvarchar Publisher "Nhà xuất bản"
        int PublishYear "Năm XB"
        decimal ImportPrice "Giá nhập"
        decimal SellPrice "Giá bán"
        int QuantityInStock "Tồn kho"
        datetime LastImportDate "Ngày nhập cuối"
        datetime LastSoldDate "Ngày bán cuối"
        bit IsDeleted "Đã xóa"
    }

    Orders {
        int OrderID PK "ID hóa đơn"
        int CustomerID FK "ID khách hàng"
        int EmployeeID FK "ID nhân viên"
        datetime OrderDate "Ngày đặt"
        decimal SubtotalAmount "Tạm tính"
        decimal DiscountAmount "Giảm giá"
        decimal TaxAmount "Thuế"
        decimal TotalAmount "Tổng tiền"
        nvarchar PaymentStatus "Trạng thái (Pending/Paid/Cancelled)"
        nvarchar PaymentMethod "Phương thức thanh toán"
        nvarchar PaymentTransactionId "Mã giao dịch"
        int LoyaltyPointsEarned "Điểm tích được"
    }

    OrderDetails {
        int OrderDetailID PK "ID chi tiết"
        int OrderID FK "ID hóa đơn"
        int BookID FK "ID sách"
        int Quantity "Số lượng"
        decimal UnitPrice "Đơn giá"
        decimal DiscountAmount "Giảm giá dòng"
        decimal Subtotal "Thành tiền"
    }
```

## Mô tả mối quan hệ

| Quan hệ | Cardinality | Diễn giải |
|---------|-------------|-----------|
| Categories → Books | 1:N | Một danh mục có nhiều sách |
| Suppliers → Books | 1:N | Một nhà cung cấp cung cấp nhiều sách |
| Books → OrderDetails | 1:N | Một sách có thể xuất hiện trong nhiều chi tiết hóa đơn |
| Orders → OrderDetails | 1:N | Một hóa đơn có nhiều dòng chi tiết |
| Customers → Orders | 1:N | Một khách hàng có thể đặt nhiều hóa đơn |
| Employees → Orders | 1:N | Một nhân viên xử lý nhiều hóa đơn |
| Employees → Accounts | 1:0..1 | Một nhân viên có thể có một tài khoản đăng nhập |
